import { Row, Col } from 'react-bootstrap';

const HomePage = () => {
    return (
        <>
            <h1>Transportathon</h1>
            <Row>
                <Col>
                    <h2>What is Transportathon?</h2>    
                    <p>Transportathon is a hackathon for transport.</p>
                </Col>
                <Col>
                    <h2>What is a hackathon?</h2>
                    <p>A hackathon is a competition where teams of people work together to solve a problem.</p>
                </Col>
            </Row>
        </>
    );

}

export default HomePage;