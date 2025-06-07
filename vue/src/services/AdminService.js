import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315/"
});

export default {
    getUsers(){
        return http.get('admin/users');
    },
    getUserReviews(id){
        return http.get(`admin/users/${id}/reviews`);
    },
    getAllReviews(){
        return http.get('admin/reviews');
    },
    updateReview(id, review){
        return http.put(`admin/reviews/${id}`, review);
    },
    deleteReview(id){
        return http.delete(`admin/reviews/${id}`);
    },
    getDrinks(){
        return http.get('admin/drinks');
    },
    updateDrink(id, drink){
        return http.put(`admin/drinks/${id}`, drink);
    }
}
