import React from 'react';
import ReactDOM from 'react-dom/client';
import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider
} from "react-router-dom"

import { Provider } from 'react-redux';
import './assets/styles/index.css';
import './assets/styles/bootstrap.custom.css';

import App from './App';
import HomePage from "./pages/HomePage";
import AddTransportRequestsPage from "./pages/AddTransportRequestPage";
import TransportRequestsPage from "./pages/TransportRequestsPage";
import AnswerTransportRequestPage from "./pages/AnswerTransportRequestPage";
import TransportRequestAnswersPage from "./pages/TransportRequestAnswersPage";
import AddBookingPage from "./pages/AddBookingPage";
import BookingsPage from "./pages/BookingsPage";
import BookingDetailPage from "./pages/BookingDetailPage";
import AddReviewPage from "./pages/AddReviewPage";
import DispalyCompanyInfo from "./pages/DisplayCompanyInfo";
import LoginPage from "./pages/LoginPage";
import RegisterPage from './pages/RegisterPage';

import PrivateRoute from './components/PrivateRoute';

import reportWebVitals from './reportWebVitals';

import store from './store';

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<App />} >
      <Route index={true} path="/" element={<HomePage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path='' element={<PrivateRoute />}>
          <Route path="/transport/requests" element={<TransportRequestsPage />} />
          <Route path="/transport/requests/:id/answers/create" element={<AnswerTransportRequestPage />} />
          <Route path="/transport/requests/:id/answers" element={<TransportRequestAnswersPage />} />
          <Route path="/transport/requests/create" element={<AddTransportRequestsPage />} />
          <Route path="/bookings" element={<BookingsPage />} />          
          <Route path="/bookings/:id" element={<BookingDetailPage />} />          
          <Route path="/bookings/:id/create" element={<AddBookingPage />} />          
          <Route path="/bookings/:id/reviews/create" element={<AddReviewPage />} />          
          <Route path="/company/:id/info" element={<DispalyCompanyInfo />} />
      </Route>
    </Route>
  ));

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Provider store={store}>
      <RouterProvider router={router} />
    </Provider>
  </React.StrictMode>
);

reportWebVitals();
