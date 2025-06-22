<script setup>
import NavBar from '@/components/NavBar.vue'
import PropertyListing from '@/components/PropertyListing.vue'
</script>

<template>
  <div>
    <NavBar />
    <div class="container">
      <div v-for="(loc, index) in listings" v-bind:key="index">
        <PropertyListing :listing="loc" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'PortalView',
  data() {
    return {
      listings: [{
        Address: '123 Main St',
        City: 'Anytown',
        State: 'CA',
        Zip: '12345',
        Country: 'USA',
        Image: 'https://th.bing.com/th/id/OIP.CNSpec0G3oD45eErHMGH7wHaE8?w=280&h=187&c=7&r=0&o=5&pid=1.7',
        Links: ['https://www.zillow.com/homedetails/123-Main-St-Anytown-CA-12345/12345678_zpid/']
      },
      {
        Address: '456 Mulligan St',
        City: 'Anytown',
        State: 'CA',
        Zip: '12345',
        Country: 'USA',
        Image: 'https://th.bing.com/th/id/OIP.FkCrWONXbI0HjEtqd9psEwHaE7?w=274&h=183&c=7&r=0&o=5&pid=1.7',
        Links: ['https://www.zillow.com/homedetails/123-Main-St-Anytown-CA-12345/12345678_zpid/']
      }]
    }
  }
}
</script>
