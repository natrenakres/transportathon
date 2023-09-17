import { useGetBookingsQuery, useCompleteBookingMutation } from '../slices/bookings.api.slice';
import Loader from '../components/Loader';
import { Table, Button } from 'react-bootstrap';
import { useSelector } from 'react-redux';
import { LinkContainer } from 'react-router-bootstrap';
import { toast } from 'react-toastify'

const BookingsPage = () => {
    const { userInfo } = useSelector(state => state.auth);
    const { data, isLoading, refetch } = useGetBookingsQuery();
    const [completeBooking, { isLoading: isCompleting }] = useCompleteBookingMutation();

    const handleComplete = async (bookingId) =>{
        console.log(`Complete ${bookingId}`);
        try {
            const data = {bookingId};
            await completeBooking(data).unwrap();
            toast.success('Booking completed successfully');
            await refetch();
        } catch (error) {
            toast.error(error?.data?.title || error?.error);
        }
    }


    return (
        <>
            <h2>Bookings</h2>
            {isCompleting ? <Loader /> : null}
            {                
                isLoading ? <Loader /> :  (
                    <>
                    <Table striped bordered hover responsive className="table-sm">
                        <thead>
                            <tr>
                                <th>Company</th>
                                <th>Begin Date</th>
                                <th>Vehicle</th>
                                <th>Status</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                data?.map(booking => (
                                    <tr key={booking.id}>                                        
                                        <td>{booking.companyName}</td>
                                        <td>{booking.beginDate}</td>
                                        <td>{booking.numberPlate}</td> 
                                        <td>{booking.status}</td>                                         
                                        <td>                                        
                                            {
                                                userInfo && !userInfo.isOwner && booking.status !== 'Completed' && (                                                
                                                    <Button variant="primary" className="btn-sm" onClick={()=> handleComplete(booking.id)}>
                                                        Complete
                                                    </Button>                                                
                                                )
                                            }                                            
                                            
                                            {
                                                userInfo && !userInfo.isOwner && booking.status === 'Completed' &&
                                                <LinkContainer to={`/bookings/${booking.id}/reviews/create`}>
                                                    <Button variant="warning" className="btn-sm">
                                                        Add Review 
                                                    </Button>
                                                </LinkContainer>

                                            }

                                            {
                                                userInfo && !userInfo.isOwner && booking.status === 'Completed' &&
                                                <LinkContainer to={`/company/${booking.companyId}/info`}>
                                                    <Button variant="warning" className="btn-sm">
                                                        Company Info
                                                    </Button>
                                                </LinkContainer>
                                            }

                            
                                            
                                            
                                        </td>
                                    </tr>
                                ))
                            }
                        </tbody>
                    </Table>                    
                    </>
                )
            }
        </>
    )
}

export default BookingsPage;
