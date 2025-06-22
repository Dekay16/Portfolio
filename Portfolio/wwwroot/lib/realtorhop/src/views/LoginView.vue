<template>
    <div class="overlay mb-4">
        <div class="row justify-content-md-center mb-4">
            <div class="col-md-12">
                <img class="mx-auto d-block" alt="Vue logo" src="../assets/img/RealtorHopLogo.svg">
            </div>
        </div>
    </div>
    <!-- Sign up or Login in -->
    <div class="container break">
        <div class="row justify-content-md-center mb-5">
            <div class="col-md-6">
                <h1 class="display-1 text-center brand-color brand-style" style="font-size:72px;">RealtorHop</h1>
                <h2 class="display-6 text-center">Skip The Realtor, We Can Help.</h2>
            </div>
        </div>
        <div class="row justify-content-md-center mb-4 break">
            <div class="col-md-6">
                <h1 class="display-6">Welcome Back!</h1>
                <form @submit.prevent="submit" ref="form needs-validation" novalidate>
                    <!-- username required field -->
                    <div class="mb-3">
                        <label for="username" class="form-label">Email</label>
                        <input type="text" class="form-control" id="username" v-model="User.email" required>
                    </div>
                    <div class="mb-3">
                        <label for="password" class="form-label">Password</label>
                        <input type="password" class="form-control" id="password" v-model="User.password" required>
                    </div>
                    <button class="btn btn-primary" type="submit">Login</button>
                </form>
            </div>
        </div>
    </div>
    <div class="justify-content-md-center break">
        <hr>
        <div class="container">
            <p class="text-center">Copyright &copy; 2024 RealtorHop. All Rights Reserved</p>
        </div>
    </div>
</template>

<script>
export default {
    name: 'LoginView',
    props: {
        msg: String
    },
    data() {
        return {
            User: {
                firstName: '',
                lastName: '',
                email: '',
                password: '',
            },
            showPasswordError: false,
        }
    },
    computed: {
        passwordConfirmed() {
            if (this.User.password == '' || this.User.passwordConfirm == '') {
                return true;
            }
            return this.User.passwordConfirm == this.User.password
        }
    },
    methods: {
        async submit() {
            if (this.passwordConfirmed) {
                this.$emit('submit', this.User);
            }
        }
    }
}
</script>

<style scoped>
.overlay {
    background-image: url("../assets/img/banner_bw_bkgd.png");
    background-color: transparent;
}

@import url('https://fonts.googleapis.com/css2?family=Nunito:wght@800&display=swap');

.brand-color {
    color: #ED155D;
}

.break {
    padding-top: 50px;
}

.brand-style {
    font-family: 'Nunito', Arial, Helvetica, sans-serif;
    font-size: 1.5em;
    font-weight: 800;
}
</style>