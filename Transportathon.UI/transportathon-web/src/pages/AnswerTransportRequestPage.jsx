import {useState} from 'react';
import { Row, Col, Form, Button} from 'react-bootstrap';
import { FaArrowLeft } from 'react-icons/fa';
import { Link, useParams, useNavigate } from 'react-router-dom';
import  FormContainer from '../components/FormContainer';
import { useAnswerTransportRequestMutation } from '../slices/transport.api.slice';
import Loader from '../components/Loader';
import { toast } from 'react-toastify'

const AnswerTransportRequestPage = () => {
    const [amount, setAmount] = useState('');
    const [currency, setCurrency] = useState('TL');
    const { id } = useParams();
    const [answerTransportRequest, { isLoading, error }] = useAnswerTransportRequestMutation();
    const navigate = useNavigate();


    const submitHandler = async (e) => {
        e.preventDefault();
        try {
            const temp = {
                id,
                data : {
                    amount,
                    currency
                }
            };

            await answerTransportRequest(temp).unwrap();
            toast.success('Transport request answer added successfully');                        
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
                    <h1>Answer Transport Request </h1>
                    {
                        isLoading ? 
                            <Loader /> 
                            : 
                        (
                            <Form onSubmit={submitHandler}>                
                                <Form.Group>
                                    <Form.Label>Price</Form.Label>
                                    <Form.Control type="number" value={amount} onChange={(e)=>setAmount(e.target.value)}/> 
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label >Transport Request Type</Form.Label>
                                    <Form.Select aria-label="Price Currency" value={currency} onChange={(e)=> setCurrency(e.target.value)}>
                                        <option value="TL">TL</option>
                                        <option value="EUR">Euro</option>                                
                                    </Form.Select>
                                </Form.Group>
                                <Button type='submit' variant='primary' className='my-3'>Submit</Button>
                            </Form>

                    )
}

                </FormContainer>

            </Col>
        </Row>
    )
}


export default AnswerTransportRequestPage;