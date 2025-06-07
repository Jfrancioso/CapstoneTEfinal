<template>
  <div class="main">
    <navigation-bar />
    <div class="container mt-4">
      <h2>New Business Rule</h2>
      <form @submit.prevent="save">
        <div class="form-group">
          <label>Name</label>
          <input v-model="rule.name" class="form-control" required>
        </div>
        <div class="form-group">
          <label>Description</label>
          <textarea v-model="rule.description" class="form-control"></textarea>
        </div>
        <div class="form-check">
          <input type="checkbox" class="form-check-input" v-model="rule.isActive" id="active">
          <label class="form-check-label" for="active">Active</label>
        </div>
        <button type="submit" class="btn btn-primary mt-2">Save</button>
      </form>
    </div>
  </div>
</template>

<script>
import navigationBar from '../components/NavigationBar.vue';
import ruleService from '../services/BusinessRuleService.js';

export default {
  components: { navigationBar },
  data() {
    return {
      rule: { name: '', description: '', isActive: true }
    };
  },
  methods: {
    save() {
      ruleService.addRule(this.rule).then(() => {
        this.$router.push('/admin');
      });
    }
  }
};
</script>
