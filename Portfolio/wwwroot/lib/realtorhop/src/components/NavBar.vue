<template>
  <div class="mb-4">
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
      <div class="container">
        <a class="navbar-brand brand-color brand-style" href="/">RealtorHop</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav"
          aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav">
            <li class="nav-item">
              <a id="portal" class="nav-link" aria-current="page" href="/portal">Portal</a>
            </li>
            <li class="nav-item">
              <a id="account" class="nav-link" href="/account">Account</a>
            </li>
            <li class="nav-item">
              <a id="support" class="nav-link" href="/support">Support</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </div>
</template>

<script>
export default {
    name: 'NavBar',
    mounted() {
      switch(this.$route.name) {
        case 'portal':
          document.getElementById('portal').classList.add('active')
          break
        case 'account':
          document.getElementById('account').classList.add('active')
          break
        case 'support':
          document.getElementById('support').classList.add('active')
          break
      }
      console.log(this.$route.name);
      console.log("test");
    }
}
</script>

<style lang="css" scoped>
@import url('https://fonts.googleapis.com/css2?family=Nunito:wght@800&display=swap');
 .brand-color {
  color: #ED155D;
 }

 .brand-style {
  font-family: 'Nunito', Arial, Helvetica, sans-serif;
  font-size: 1.5em;
  font-weight: 800;
 }
</style>