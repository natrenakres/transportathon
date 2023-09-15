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
        answerTransportRequest: builder.mutation({
            query: (data) => ({
                url: `${BASE_URL}/api/transport/request/${data.id}/answer`,
                method: 'POST',
                body: data.data,
            })
        }),
        getTransportRequestAnswers: builder.query({
            query: (id) => ({
                url: `${BASE_URL}/api/transport/request/${id}/answers`,
                method: 'GET',
            })
        }),
        acceptTransportRequestAnswer: builder.mutation({
            query: (data) => ({
                url: `${BASE_URL}/api/transport/request/${data.id}/answer/${data.answerId}/accept`,
                method: 'PUT',
            })
        }),
    })
});


export const { 
    useAddTransportRequestMutation, 
    useGetMemberTransportRequestsQuery, 
    useAnswerTransportRequestMutation,
    useGetTransportRequestAnswersQuery,
    useAcceptTransportRequestAnswerMutation
 } = transportApiSlice;