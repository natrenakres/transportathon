import { useParams } from 'react-router-dom';
import { Button, Col, Row, Table } from 'react-bootstrap';
import { useGetCompanyBookingsQuery } from '../slices/user.api.slice';
import Loader from '../components/Loader';

const DispalyCompanyInfo = () => {
    const { id: companyId } = useParams();

    const { data, isLoading } = useGetCompanyBookingsQuery(companyId);


    return (
        <div>
            <h1>Display Company Info</h1>
            {
                isLoading ? <Loader /> : (
                    data?.map(booking => (
                        <Table key={booking.id} striped bordered hover responsive className="table-sm">
                            <thead>
                                <tr>
                                    <th>Vehicle</th>
                                    <th>Driver</th>
                                    <th>Carriers</th>
                                    <th>Reviews</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                   <td>
                                    ({ booking.vehicle.numberPlate }) -
                                    { booking.vehicle.model } -
                                    { booking.vehicle.type } -
                                    { booking.vehicle.year } 
                                   </td> 
                                   <td>
                                       { booking.vehicle.driver }
                                   </td>
                                   <td>
                                    { booking.carriers?.map(carrier => (
                                            <div key={carrier.id}>
                                                { carrier.name } -
                                                Experience: { carrier.experience } -
                                                Profession: { carrier.profession } -
                                            </div>
                                        ))
                                    } 
                                   </td>
                                   <td>
                                    { booking.reviews?.map(review => (
                                            <div key={review.id}>
                                                Commend: { review.comment } -
                                                Rating: { review.rating } <br />
                                                User: { review.userName} - 
                                                {new Date(review.createdOnUtc).toLocaleDateString()}
                                            </div>
                                        ))
                                    }
                                   </td>
                                </tr>
                            </tbody>
                        </Table>
                    ))
                )

            }
        </div>
    )
}

export default DispalyCompanyInfo;