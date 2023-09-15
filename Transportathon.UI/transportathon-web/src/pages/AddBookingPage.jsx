import { useState } from 'react';
import { useGetCompanyInfoQuery } from '../slices/user.api.slice';
import { useReserveBookingMutation } from '../slices/bookings.api.slice';
import { useParams } from 'react-router-dom';
import { Form, Button, Col, Row } from 'react-bootstrap';
import { useNavigate, Link } from 'react-router-dom';
import { toast } from 'react-toastify'
import FormContainer from '../components/FormContainer.jsx';
import Loader from '../components/Loader'
import Message from '../components/Message';
import { FaArrowLeft } from 'react-icons/fa'


const AddBookingPage = () => {    
    const [vehicle, setVehicle] = useState();

    const { data, isLoading, } = useGetCompanyInfoQuery();
    const { id } = useParams();
    const [reserveBooking, { isLoading: isReserving }] = useReserveBookingMutation();
    const navigate = useNavigate();

    const submitHandler = async (e) => {
        e.preventDefault();
        if(!vehicle){
            toast.error('Please select vehicle');
            return;
        }

        const data = {
            transportRequestId: id,
            vehicleId: vehicle
        };
        try {
            await reserveBooking(data).unwrap();
            toast.success('Booking added successfully');
            navigate('/bookings');
        }
        catch (error) {            
            toast.error(error?.data?.title || error?.data?.name || error?.error);
        }
        
    }

    return (
        <Row>
            <Col>
                <Link to='/bookings' className='btn btn-light my-3'><FaArrowLeft /> Go Back</Link>            
                <FormContainer>                    
                    {isLoading && <Loader />}
                    {isReserving && <Loader />}
                    <h2>Add Booking for {data?.name}</h2>
                    <Form onSubmit={submitHandler}>                        
                        <Form.Group>
                            <Form.Label >Vehicles</Form.Label>
                            <Form.Select aria-label="Vehicles" onChange={(e)=> setVehicle(e.target.value)}>
                                <option>Select Vehicle</option>
                                {
                                    data?.vehicles?.map(vehicle => (                                        
                                        <option key={vehicle.id} value={vehicle.id}>{vehicle.numberPlate}</option>
                                    ))                                    
                                }
                            </Form.Select>
                        </Form.Group>
                        <Button type='submit' variant='primary' className='my-3'>Submit</Button>
                    </Form>
                </FormContainer>
            </Col>
        </Row>
    )
}


export default AddBookingPage;