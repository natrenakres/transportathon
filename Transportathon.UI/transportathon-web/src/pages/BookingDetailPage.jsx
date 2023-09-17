import { useParams } from 'react-router-dom';
import { useGetBookingQuery } from '../slices/bookings.api.slice';
import Loader from '../components/Loader';

const BookingDetailPage = () => {
    const { id } = useParams();
    const { data, isLoading } = useGetBookingQuery(id);
    return (
        <div>
            <h1>Booking Detail Page</h1>
            {
                isLoading ? <Loader /> : (
                    <div>
                        <h2>Company: {data?.companyName}</h2>
                        <h2>Vehicle: {data?.numberPlate}</h2>
                        <h2>Begin Date: {data?.beginDate}</h2>
                        <h2>End Date: {data?.endDate}</h2>
                        <h2>Status: {data?.status}</h2>
                    </div>
                )
            }
        </div>
    )
}

export default BookingDetailPage;