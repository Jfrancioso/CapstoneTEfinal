<template>
  <div class="main">
    <navigation-bar />
    <div class="container mt-4">
      <h2>Administrator Dashboard</h2>
      <p>Total Business Rules: {{ rules.length }}</p>
      <ul>
        <li v-for="rule in rules" :key="rule.id">
          {{ rule.name }} - <router-link :to="{ name: 'edit-rule', params: { id: rule.id } }">Edit</router-link>
        </li>
      </ul>
      <button @click="createNew" class="btn btn-primary">Add Rule</button>
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
      rules: []
    };
  },
  created() {
    ruleService.getRules().then(r => {
      this.rules = r.data;
    });
  },
  methods: {
    createNew() {
      this.$router.push({ name: 'add-rule' });
    }
  }
};
</script>

<style scoped>
.container {
  max-width: 600px;
}
</style>
