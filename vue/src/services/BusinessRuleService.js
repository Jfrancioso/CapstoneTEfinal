import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315/"
});

export default {
    getRules() {
        return http.get('businessrule');
    },
    getRule(id) {
        return http.get(`businessrule/${id}`);
    },
    addRule(rule) {
        return http.post('businessrule', rule);
    },
    updateRule(id, rule) {
        return http.put(`businessrule/${id}`, rule);
    },
    deleteRule(id) {
        return http.delete(`businessrule/${id}`);
    }
};
