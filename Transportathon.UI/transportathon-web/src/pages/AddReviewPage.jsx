import { useState } from 'react';
import { useParams } from 'react-router-dom';
import { Form, Button, Col, Row } from 'react-bootstrap';
import { useNavigate, Link } from 'react-router-dom';
import { toast } from 'react-toastify'
import FormContainer from '../components/FormContainer.jsx';
import Loader from '../components/Loader'
import Message from '../components/Message';
import { FaArrowLeft } from 'react-icons/fa';
import { useAddReviewMutation } from "../slices/review.api.slice.js";

const AddReviewPage = () => {
    const [rating, setRating] = useState();
    const [comment, setComment] = useState();
    const { id } = useParams();
    const [addReview, { isLoading }] = useAddReviewMutation();
    const navigate = useNavigate();

    const submitHandler = async (e) => {
        e.preventDefault();
        const data = {
            rating,
            comment,
            bookingId: id,
        };
        try {
            await addReview(data).unwrap();
            toast.success('Review added successfully');
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
                    <h2>Add Review </h2>
                    <Form onSubmit={submitHandler}>                        
                        <Form.Group>
                            <Form.Label >Rating</Form.Label>
                            <Form.Control type='number' onChange={(e)=> setRating(e.target.value)}>                                
                            </Form.Control>
                        </Form.Group>
                        <Form.Group>
                            <Form.Label >Commend</Form.Label>
                            <Form.Control onChange={(e)=> setComment(e.target.value)}>                                
                            </Form.Control>
                        </Form.Group>
                        <Button type='submit' variant='primary' className='my-3'>Submit</Button>
                    </Form>
                </FormContainer>
            </Col>
        </Row>
    )
}

export default AddReviewPage;