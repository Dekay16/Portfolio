<template>
    <div class="overlay mb-4">
        <div class="row justify-content-md-center mb-4">
            <div class="col-md-12">
                <img class="mx-auto d-block" alt="Vue logo" src="../assets/img/RealtorHopLogo.svg">
            </div>
        </div>
    </div>
    <!-- Sign up or Login in -->
    <div class="break">
        <div class="row justify-content-md-center mb-5">
            <div class="col-md-6">
                <h1 class="display-1 text-center brand-color brand-style" style="font-size:72px;">RealtorHop</h1>
                <h2 class="display-6 text-center">Skip The Realtor, We Can Help</h2>
                <h2 class="display-6 text-center break">Dont Give Up Your Equity</h2>
                <h2 class="display-6 text-center">Save An Average Of $27,000*</h2>
            </div>
        </div>
        <div class="row justify-content-md-center mb-5">
            <div class="col-md-6">
                <p class="lead text-center">Realtor Hop is a new way to buy and sell homes. We connect buyers to
                    sellers directly. Using home automation Realtor Hop will show buyers your home. Cut out the
                    middle man, we'll show you how.</p>
            </div>
        </div>
    </div>
    <div class="justify-content-md-center text-center pad-below">
        <a class="btn btn-lg btn-danger" href="/signup">Sign Me Up!</a>
    </div>
    <div class="bg-light">
        <div class="row justify-content-md-center break">
            <div class="col-md-2 text-center">
                <img class="bd-placeholder-img rounded-circle" width="140" height="140"
                    src="../assets/img/testimonials/bryant_cindy.webp" alt="Bryant and Cindy">
                <h2 class="fw-normal break">Saved $18,000</h2>
                <p>"We were always scared to list without an agent, but wow! That was easy and we saved so much equity."</p>
                    <p>-John & Gina</p>
            </div><!-- /.col-lg-4 -->
            <div class="col-md-2 text-center">
                <img class="bd-placeholder-img rounded-circle" width="140" height="140"
                    src="../assets/img/testimonials/David_Jennifer.webp" alt="Bryant and Cindy">
                <h2 class="fw-normal break">Saved $32,000</h2>
                <p>"Dealing directly with the buyers there was no lengthy back and forth and there was no 
                    pressure to sell on the agents terms."</p>
                    <p>-Sara & Mike</p>
            </div><!-- /.col-lg-4 -->
            <div class="col-md-2 text-center">
                <img class="bd-placeholder-img rounded-circle" width="140" height="140"
                    src="../assets/img/testimonials/john_gina.webp" alt="Bryant and Cindy">
                <h2 class="fw-normal break">Saved $8,000</h2>
                <p>"Without an agent we had complete control and it was easy."</p>
                <p>-Tom & Lisa</p>
            </div><!-- /.col-lg-4 -->
        </div>
    </div>
    <div class="justify-content-md-center break">
            <hr>
            <div class="container">
                <p class="text-center">*Based on the average home value of $452,510 USD and the average realtor fee
                    percentage of 6%.</p>
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
        validUsername() {
            if (!this.isSubmitted) {
                return true;
            }
            return this.User.username.length > 0;
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
                console.log('submitting');
                console.log(this.$rest_api);
                // Post user to API

                //this.$emit('submit', this.User);
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

.pad-below {
    padding-bottom: 50px;
}

.brand-style {
    font-family: 'Nunito', Arial, Helvetica, sans-serif;
    font-size: 1.5em;
    font-weight: 800;
}
</style>