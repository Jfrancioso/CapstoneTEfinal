import axios from 'axios';

// Use the same base URL as the rest of the services so requests
// are always directed to the backend API rather than the dev server.
const http = axios.create({
    baseURL: process.env.VUE_APP_REMOTE_API
});

export default {
    GetBusinessByNameAndZip(yelpinput) {
        return http.post('/YelpApi/', yelpinput);
    }
}