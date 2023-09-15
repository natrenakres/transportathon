import { useParams } from "react-router-dom";
import { useGetTransportRequestAnswersQuery, useAcceptTransportRequestAnswerMutation } from "../slices/transport.api.slice";
import { Table, Button } from 'react-bootstrap';
import Message from '../components/Message';
import Loader from '../components/Loader';
import { toast } from 'react-toastify'

const TransportRequestAnswersPage = () => {
    const { id } = useParams();
    const { data, isLoading, refetch } = useGetTransportRequestAnswersQuery(id);
    const [acceptTransportRequestAnswer, { isLoading: isAccepting, }] = useAcceptTransportRequestAnswerMutation();

    const handleAccept = async (answerId) =>{
        console.log(`Accept ${answerId}`);
        try {
            const data = {id, answerId};
            await acceptTransportRequestAnswer(data).unwrap();
            toast.success('Transport request answer accepted successfully');
            refetch();

        } catch (error) {
            toast.error(error?.data?.title || error?.error);
        }
    }

    return (
        <>
            <h1>Transport Request Answers </h1>
            {
                isAccepting ? <Loader /> : null
            }
            {
                isLoading ? <Loader /> : (
                    <>
                    <Table striped bordered hover responsive className="table-sm">
                        <thead>
                            <tr>
                                <th>Campany</th>
                                <th>Price</th>                                
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                data?.map(answer => (
                                    <tr key={answer.id}>                                        
                                        <td>{answer.companyName}</td>
                                        <td>{answer.amount} {answer.currency}</td>
                                        <td>    
                                            <Button variant="primary" 
                                            disabled={answer.isAcceptedFromMember}
                                            className="btn-sm" onClick={() => handleAccept(answer.id)}>
                                                Accept
                                            </Button>                                        
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

export default TransportRequestAnswersPage;