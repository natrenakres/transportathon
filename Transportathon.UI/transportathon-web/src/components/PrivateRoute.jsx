import { Outlet, Navigate } from "react-router-dom";
import { useSelector } from "react-redux";

const PrivateRoute = () => {
    const { userInfo } = useSelector(state => state.auth);
    if (!userInfo) {
        return <Navigate to='/login' />;
    }

    return (
        <Outlet />
    );
}


export default PrivateRoute;