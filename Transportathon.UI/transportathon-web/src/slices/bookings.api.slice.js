import { apiSlice } from './api.slice';
import { BASE_URL } from '../constants';

export const transportApiSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        reserveBooking: builder.mutation({
            query: (data) => ({
                url: `${BASE_URL}/api/bookings`,
                method: 'POST',
                body: data,
            })
        }),
        completeBooking: builder.mutation({
            query: (data) => ({
                url: `${BASE_URL}/api/bookings/${data.bookingId}`,
                method: 'PUT'
            })
        }),
        getBookings: builder.query({
            query: () => ({
                url: `${BASE_URL}/api/bookings`,
                method: 'GET',
            })
        }),
        getBooking: builder.query({
            query: (bookingId) => ({
                url: `${BASE_URL}/api/bookings/${bookingId}`,
                method: 'GET',
            })
        }),    
    })
});


export const { 
    useReserveBookingMutation,
    useCompleteBookingMutation,
    useGetBookingsQuery,
    useGetBookingQuery
 } = transportApiSlice;