import { apiSlice } from './api.slice';
import { BASE_URL } from '../constants';

export const transportApiSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        addTransportRequest: builder.mutation({
            query: (data) => ({
                url: `${BASE_URL}/api/transport/request`,
                method: 'POST',
                body: data,
            })
        }),
        getMemberTransportRequests: builder.query({
            query: () => ({
                url: `${BASE_URL}/api/transport/request`,
                method: 'GET',
            })
        }),
    })
});


export const { useAddTransportRequestMutation, useGetMemberTransportRequestsQuery } = transportApiSlice;