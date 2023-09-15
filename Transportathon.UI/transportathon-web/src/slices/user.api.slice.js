import { USERS_URL } from '../constants';    
import { apiSlice } from './api.slice';

export const usersApiSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        login: builder.mutation({
            query: (data) => ({
                url: `${USERS_URL}/login`,
                method: 'POST',
                body: data,
            })
        }),
        register: builder.mutation({
            query: (data) => ({
                url: `${USERS_URL}/register`,
                method: 'POST',
                body: data,
            })
        }),
        logout: builder.mutation({
            query: () => ({
                url: `${USERS_URL}/logout`,
                method: 'POST',
            })
        }),
        getCompanyInfo: builder.query({
            query: () => ({
                url: `${USERS_URL}/company/info`,
                method: 'GET',
            })
        }),
    }),
});



export const { useLoginMutation, useLogoutMutation, useRegisterMutation, useGetCompanyInfoQuery} = usersApiSlice;

