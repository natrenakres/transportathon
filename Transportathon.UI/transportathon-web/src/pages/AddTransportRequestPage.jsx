import { useState } from 'react';
import FormContainer from '../components/FormContainer.jsx';
import { FaArrowLeft } from 'react-icons/fa'
import Loader from '../components/Loader'
import Message from '../components/Message'
import { Form, Button, Col, Row } from 'react-bootstrap';
import { useNavigate, Link } from 'react-router-dom';
import { toast } from 'react-toastify'
import { useAddTransportRequestMutation } from "../slices/transport.api.slice.js"

const AddTransportRequestPage = () => {
    const [beginDate, setBeginDate] = useState('');
    const [description, setDescription] = useState('');
    const [transportRequestType, setTransportRequestType] = useState(1);
    const [address, setAddress] = useState('');
    const [city, setCity] = useState('');
    const [state, setState] = useState('');
    const [zip, setZip] = useState('');
    const [country, setCountry] = useState('');

    const [addTransportRequest, { isLoading, error, isSuccess }] = useAddTransportRequestMutation();

    console.log(error);
    
    const navigate = useNavigate();

    const submitHandler = async (e) => {
        e.preventDefault();
        try {
            const data = {
                beginDate,
                description,
                type: transportRequestType,
                address : {
                    street: address,
                    city,
                    state,
                    zipCode: zip,
                    country
                }
            };
            await addTransportRequest(data).unwrap();
            toast.success('Transport Request added successfully');                        
            navigate('/transport/requests');
        } catch (error) {
            toast.error(error?.data?.title || error?.error);
        }     
    }

    return (
        <Row>
            <Col>
                <Link to='/transport/requests' className='btn btn-light my-3'><FaArrowLeft /> Go Back</Link>            
                <FormContainer>                    
                    <h1>Add Transport Request</h1>
                    {isLoading && <Loader />}
                    {error && <Message variant='danger'>
                        {error?.data?.title}
                    </Message>}
                    {isSuccess && <Message variant='success'>Transport Request added successfully</Message>}
                    {isLoading ? <Loader /> : (
                    <Form onSubmit={submitHandler}>                
                        <Form.Group>
                            <Form.Label>Begin Date</Form.Label>
                            <Form.Control type="date" placeholder="01.01.2023" value={beginDate} onChange={(e)=>setBeginDate(e.target.value)}/> 
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Description</Form.Label>
                            <Form.Control as="textarea" rows={3} value={description} onChange={(e)=> setDescription(e.target.value)}/>
                        </Form.Group>
                        <Form.Group>
                            <Form.Label >Transport Request Type</Form.Label>
                            <Form.Select aria-label="Transport Request Type" onChange={(e)=> setTransportRequestType(e.target.value)}>
                                <option value="1">HomeToHome</option>
                                <option value="2">OfficeTransport</option>
                                <option value="3">BigVolumeAndHeavy</option>
                            </Form.Select>
                        </Form.Group>
                        <Form.Group className="mb-3" controlId="formGridAddress1">
                            <Form.Label>Address</Form.Label>
                            <Form.Control placeholder="1234 Main St" value={address} onChange={(e)=>setAddress(e.target.value)} />
                        </Form.Group>                

                        <Row className="mb-3">
                            <Form.Group as={Col} controlId="formGridCity">
                                <Form.Label>City</Form.Label>
                                <Form.Control value={city} onChange={(e)=> setCity(e.target.value)} />
                            </Form.Group>

                            <Form.Group as={Col} controlId="formGridState">
                                <Form.Label>State</Form.Label>
                                <Form.Control value={state} onChange={(e)=> setState(e.target.value)} />
                            </Form.Group>

                            <Form.Group as={Col} controlId="formGridZip">
                                <Form.Label>Zip</Form.Label>
                                <Form.Control value={zip} onChange={(e)=> setZip(e.target.value)} />
                            </Form.Group>
                        </Row>

                        <Form.Group className="mb-3" id="formGridCheckbox">
                            <Form.Label>Country</Form.Label>
                            <Form.Control value={country} onChange={(e)=> setCountry(e.target.value)} />
                        </Form.Group>
                        <Button type='submit' variant='primary' className='my-3'>Submit</Button>
                    </Form>
                    )}
                </FormContainer>
            </Col>
        </Row>
    );
}

export default AddTransportRequestPage;