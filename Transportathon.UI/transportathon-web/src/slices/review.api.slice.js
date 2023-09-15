import { apiSlice } from './api.slice';
import { BASE_URL } from '../constants';

export const transportApiSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        addReview: builder.mutation({
            query: (data) => ({
                url: `${BASE_URL}/api/reviews`,
                method: 'POST',
                body: data,
            })
        }),        
    })
});


export const { 
    useAddReviewMutation,    
 } = transportApiSlice;