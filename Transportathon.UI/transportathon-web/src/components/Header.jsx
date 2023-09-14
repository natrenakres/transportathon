import { Navbar, Nav, Container, NavDropdown } from 'react-bootstrap';
import { FaUser } from 'react-icons/fa';
import { LinkContainer } from 'react-router-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { useLogoutMutation } from '../slices/user.api.slice';
import { logout } from '../slices/auth.slice';

const Header = () => {
    const { userInfo } = useSelector(state => state.auth);

    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [logoutApiCall, { isLoading }] = useLogoutMutation();

    const logoutHandler = async () => {
        try {
            await logoutApiCall().unwrap();
            dispatch(logout());
            navigate('/login');
        }
        catch (err) {
            console.log(err);
        }
    }

    return (
        <header>
            <Navbar bg="dark" variant="dark" expand="md" collapseOnSelect>
                <Container>
                    <LinkContainer to="/">
                        <Navbar.Brand href="/">Transportathon</Navbar.Brand>
                    </LinkContainer>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="ms-auto">
                            <Nav.Link href="/transport-requests">Transport Requests</Nav.Link>
                            <Nav.Link href="/contact">Contact</Nav.Link>
                            { userInfo ? (
                                <>
                                    <NavDropdown title={userInfo.name} id="username">
                                        <LinkContainer to='/profile'>
                                            <NavDropdown.Item>Profile</NavDropdown.Item>
                                        </LinkContainer>                                    
                                        <NavDropdown.Item onClick={logoutHandler}>Logout</NavDropdown.Item>
                                    </NavDropdown>
                                    {
                                        userInfo.isAdmin && (
                                            <>
                                                <NavDropdown title='Admin' id="admin">
                                                    <LinkContainer to='/admin/product-list'>
                                                        <NavDropdown.Item>Products</NavDropdown.Item>
                                                    </LinkContainer>
                                                    <LinkContainer to='/admin/order-list'>                                                                        
                                                        <NavDropdown.Item>Orders</NavDropdown.Item>
                                                    </LinkContainer>
                                                    <LinkContainer to='/admin/user-list'>                                                                        
                                                        <NavDropdown.Item>Users</NavDropdown.Item>
                                                    </LinkContainer>
                                                </NavDropdown>
                                            </>
                                            )
                                    }                                    
                                </>

                            ) : (<LinkContainer to='/login'>
                                <Nav.Link href="/login"><FaUser /> Sign In</Nav.Link>
                            </LinkContainer>)}                            
                        </Nav>                        
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </header>
    )
}

export default Header;