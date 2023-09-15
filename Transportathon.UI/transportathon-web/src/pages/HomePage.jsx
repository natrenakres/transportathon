import { Row, Col, Button } from 'react-bootstrap';
import { useNavigate, Link } from "react-router-dom";
import { LinkContainer } from 'react-router-bootstrap';

const HomePage = () => {
    const navigate = useNavigate();
    const createTransportRequest = () => {
        navigate('/transport/requests/create');
    }

    return (
        <>
            <h1>Transportathon</h1>
            <Row>
                <Col>                    
                    <p>
                        Do you need a vehicle and team for transportation? 
                        You can sign up to our system, create a transport request then choose a answers and wait a reservation.
                        After that you can see your booking details.
                    </p>
                </Col>                
            </Row>
            <Row>
                <Col>
                            <Button type="button" className="btn-block" 
                                onClick={createTransportRequest}>
                                Create Transport Request
                            </Button>                            
                </Col>
            </Row>
            <Row className='mt-3'>
                <Col>
                    <LinkContainer to="/transport/requests/">
                                <Button type="button" className="btn-block" >Transport Requests</Button>
                            </LinkContainer>
                </Col>
            </Row>

        </>
    );

}

export default HomePage;