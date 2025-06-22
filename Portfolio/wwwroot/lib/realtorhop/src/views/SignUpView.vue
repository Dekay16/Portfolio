<template>
    <!--<div class="overlay mb-4">
        <div class="row justify-content-md-center mb-4">
            <div class="col-md-12">
                <img class="mx-auto d-block" alt="Vue logo" src="../assets/img/RealtorHopLogo.svg">
            </div>
        </div>
    </div>-->
    <!-- Sign up or Login in -->
    <div class="container break">
        <div class="row justify-content-md-center mb-5">
            <div class="col-md-6">
                <h1 class="display-1 text-center brand-color brand-style" style="font-size:72px;">RealtorHop</h1>
                <h2 class="display-6 text-center">Skip The Realtor, We Can Help</h2>
            </div>
        </div>
        <div class="row justify-content-md-center mb-4 break">
            <div class="col-md-6">
                <h1 class="display-6">Sign Up</h1>
                <form @submit.prevent="submit" ref="form needs-validation" novalidate>
                    <!-- username required field -->
                    <div class="mb-3">
                        <label for="exampleInputEmail1" class="form-label">Email address</label>
                        <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp"
                            v-model="User.email" required>
                        <div class="form-text brand-color" v-if="!validEmail">Email is required</div>
                    </div>
                    <div class="mb-3">
                        <label for="firstName" class="form-label">First Name</label>
                        <input type="text" class="form-control" id="firstName" v-model="User.firstName" required>
                        <div class="form-text brand-color" v-if="!validFirstName">First Name is required</div>
                    </div>
                    <div class="mb-3">
                        <label for="lastName" class="form-label">Last Name</label>
                        <input type="text" class="form-control" id="lastName" v-model="User.lastName" required>
                        <div class="form-text brand-color" v-if="!validLastName">Last Name is required</div>
                    </div>
                    <div class="mb-3">
                        <label for="password" class="form-label">Password</label>
                        <input type="password" class="form-control" id="password" v-model="User.password" required>
                        <div class="form-text brand-color" v-if="!validPassword">Password must be at least 12 characters
                            long</div>
                    </div>
                    <div class="mb-3">
                        <label for="passwordConfirm" class="form-label">Confirm Password</label>
                        <input type="password" class="form-control" id="passwordConfirm" v-model="passwordConfirm" required>
                        <div class="form-text brand-color" v-if="!passwordConfirmed">Passwords do not match</div>
                    </div>
                    <button class="btn btn-danger" type="submit">Get Started</button>
                </form>
            </div>
        </div>
        <div class="row justify-content-md-center">
            <div class="col-md-6">
                <hr>
                <p class="lead text-center">Already have an account? <a href="/login">Login Here</a></p>
            </div>
        </div>
    </div>
</template>

<script>

export default {
    name: 'HomeView',
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
            passwordConfirm: '',
            submitted: false
        }
    },
    computed: {
        passwordConfirmed() {
            if (this.passwordConfirm.length != this.User.password.length) {
                return true;
            }
            return this.passwordConfirm == this.User.password
        },
        validFirstName() {
            if (!this.isSubmitted) {
                return true;
            }
            return this.User.firstName.length > 0;
        },
        validLastName() {
            if (!this.isSubmitted) {
                return true;
            }
            return this.User.lastName.length > 0;
        },
        validEmail() {
            if (!this.isSubmitted) {
                return true;
            }
            return this.User.email.length > 0;
        },
        validPassword() {
            if (!this.isSubmitted) {
                return true;
            }
            return this.User.password.length > 11;
        },
        isSubmitted() {
            return this.submitted;
        }
    },
    methods: {
        submit() {
            console.log(this.User);
            this.submitted = true;
            if (this.passwordConfirmed & 
            this.validUsername & 
            this.validFirstName & 
            this.validLastName & 
            this.validEmail & 
            this.validPassword) {
                // Post user to API
                this.ajaxRequest = true;
                this.$http.post('http://localhost:3000/api/users', this.User)
                    .then(response => {
                        console.log(response);
                        this.ajaxRequest = false;
                        this.$router.push('/login');
                    })
                    .catch(error => {
                        console.log(error);
                        this.ajaxRequest = false;
                    });
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