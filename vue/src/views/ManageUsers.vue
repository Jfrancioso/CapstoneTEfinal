<template>
  <div class="main">
    <navigation-bar />
    <div class="container mt-4">
      <h2>Manage Posts</h2>
      <div v-for="user in users" :key="user.userId" class="mb-4">
        <h5>{{ user.username }}</h5>
        <button class="btn btn-sm btn-secondary mb-2" @click="loadReviews(user.userId)">Load Reviews</button>
        <ul v-if="reviews[user.userId]">
          <li v-for="rev in reviews[user.userId]" :key="rev.review_ID" class="mb-2">
            <textarea v-model="rev.reviewText" class="form-control mb-1"></textarea>
            <input v-model="rev.rating" class="form-control mb-1" />
            <button class="btn btn-primary btn-sm mr-1" @click="updateReview(rev)">Approve/Edit</button>
            <button class="btn btn-danger btn-sm" @click="deleteReview(rev.review_ID)">Delete</button>
          </li>
        </ul>
      </div>
      <div class="mt-5">
        <h4>Pending Drinks</h4>
        <ul>
          <li v-for="drink in drinks" :key="drink.drinkID" class="mb-2">
            <input v-model="drink.name" class="form-control mb-1" />
            <textarea v-model="drink.description" class="form-control mb-1"></textarea>
            <label><input type="checkbox" v-model="drink.isApproved" /> Approved</label>
            <button class="btn btn-primary btn-sm mr-1" @click="updateDrink(drink)">Save</button>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
import navigationBar from '../components/NavigationBar.vue';
import adminService from '../services/AdminService.js';

export default {
  components: { navigationBar },
  data() {
    return {
      users: [],
      reviews: {},
      drinks: []
    };
  },
  created() {
    adminService.getUsers().then(r => {
      this.users = r.data;
    });
    adminService.getDrinks().then(r => {
      this.drinks = r.data.filter(d => !d.isApproved);
    });
  },
  methods: {
    loadReviews(id) {
      adminService.getUserReviews(id).then(r => {
        this.$set(this.reviews, id, r.data);
      });
    },
    updateReview(review) {
      adminService.updateReview(review.review_ID, review).then(r => {
        Object.assign(review, r.data);
      });
    },
    deleteReview(id) {
      adminService.deleteReview(id).then(() => {
        for (const key in this.reviews) {
          this.reviews[key] = this.reviews[key].filter(r => r.review_ID !== id);
        }
      });
    },
    updateDrink(drink) {
      adminService.updateDrink(drink.drinkID, drink).then(r => {
        Object.assign(drink, r.data);
        this.drinks = this.drinks.filter(d => !d.isApproved);
      });
    }
  }
};
</script>

<style scoped>
.container {
  max-width: 800px;
}
</style>
