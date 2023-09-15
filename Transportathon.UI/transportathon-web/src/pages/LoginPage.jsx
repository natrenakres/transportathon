import { useState, useEffect } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { Form, Button, Row, Col } from "react-bootstrap";

import { useLoginMutation } from "../slices/user.api.slice.js";
import { setCredentials } from "../slices/auth.slice";
import FormContainer from "../components/FormContainer"
import Loader from "../components/Loader";
import { toast } from "react-toastify";


const LoginPage = () =>{
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const {search } = useLocation();
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const [login, { isLoading }] = useLoginMutation();
    const { userInfo } = useSelector((state) => state.auth);

    const sp = new URLSearchParams(search);
    const redirect = sp.get('redirect') || '/';

    useEffect(() => {        
        if (userInfo) {
            navigate(redirect);
        }
    }, [userInfo, navigate, redirect]);


    const submitHandler = async (e) => {
        e.preventDefault();
        try {
            const res = await login({ email, password }).unwrap();
            dispatch(setCredentials({...res}));
            toast.success('Login successful');
            navigate(redirect);
        }
        catch (err) {
            toast.error(err?.data?.message || err?.error);
        }
    }

    return (
        <FormContainer>
            <h1>Sign In</h1>
            <Form onSubmit={submitHandler}>
                <Form.Group controlId='email' className="my-3">
                    <Form.Label>Email Address</Form.Label>
                    <Form.Control
                        type='email'
                        placeholder='Enter Email'
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    ></Form.Control>
                </Form.Group>
                <Form.Group controlId='password' className="my-3">
                    <Form.Label>Password</Form.Label>
                    <Form.Control
                        type='password'
                        placeholder='Enter password'
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    ></Form.Control>
                </Form.Group>
                <Button type='submit' variant='primary' className="mt-2" disabled={isLoading}>Sign In</Button>
                {isLoading && <Loader />}
            </Form>
            <Row className='py-3'>
                <Col>   
                    New Customer? <Link to={redirect ? `/register?redirect=${redirect}` : '/register'}>Register</Link>
                </Col>
            </Row>
        </FormContainer>
    )
}

export default LoginPage;