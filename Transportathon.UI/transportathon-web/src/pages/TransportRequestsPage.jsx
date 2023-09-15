import { useSelector } from 'react-redux';
import { useGetMemberTransportRequestsQuery } from '../slices/transport.api.slice';
import { LinkContainer } from 'react-router-bootstrap';
import { Table, Button } from 'react-bootstrap';
import Message from '../components/Message';
import Loader from '../components/Loader';

const TransportRequestsPage = () => {
    const { userInfo } = useSelector(state => state.auth);
    const { data, isLoading, error } = useGetMemberTransportRequestsQuery();

    return (
        <>
            <h1>Transport Requests</h1>
            {
                isLoading ? <Loader /> : error ? <Message variant='danger'>{error}</Message> : (
                    <>
                    <Table striped bordered hover responsive className="table-sm">
                        <thead>
                            <tr>
                                <th>Type</th>
                                <th>Begin Date</th>
                                <th>Status</th>
                                {
                                    userInfo && userInfo.isOwner && (
                                        <>
                                            <th>Description</th>
                                            <th>Address</th>
                                        </>
                                    )
                                }
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                data?.map(transportRequest => (
                                    <tr key={transportRequest.id}>                                        
                                        <td>{transportRequest.type}</td>
                                        <td>{transportRequest.beginDate}</td>
                                        <td>{transportRequest.status}</td>  
                                        {userInfo && userInfo.isOwner && (
                                            <>
                                                <td>{transportRequest.description}</td>
                                                <td>{transportRequest.address.street}  {transportRequest.address.city}  {transportRequest.address.country}</td>
                                            </>
                                        )                                      }
                                        <td>
                                        {
                                            userInfo && !userInfo.isOwner && (
                                                <LinkContainer to={`/transport/requests/${transportRequest.id}/answers`}>
                                                    <Button variant="light" className="btn-sm">
                                                        Answers
                                                    </Button>
                                                </LinkContainer>
                                            )
                                        }
                                            
                                            {
                                                transportRequest.status === 'Booked' && (
                                                    <LinkContainer to={`/bookings/${transportRequest.id}`}>
                                                        <Button variant="light" className="btn-sm">
                                                            Bookings
                                                        </Button>
                                                    </LinkContainer>
                                                    )
                                            }
                                            
                                            {
                                                userInfo && userInfo.isOwner &&
                                                <LinkContainer to={`/transport/requests/${transportRequest.id}/answers/create`}>
                                                    <Button variant="warning" className="btn-sm">
                                                        Answer
                                                    </Button>
                                                </LinkContainer>

                                            }
                                            {
                                                transportRequest.status === 'Booked' && (
                                                    <LinkContainer to={`/bookings/${transportRequest.id}/create`}>
                                                        <Button variant="warning" className="btn-sm">
                                                           Add Booking
                                                        </Button>
                                                    </LinkContainer>
                                                )
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

export default TransportRequestsPage