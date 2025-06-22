<template>
    <div class="card">
        <div class="row g-0">
            <div class="col-md-4" style="max-width: 160px;">
                <div :style="{
                    'height': '100%',
                    'background-image': 'url(' + listing.Image + ')',
                    'background-repeat': 'no-repeat',
                    'background-position': 'center center'
                }">
                </div>
            </div>
            <div class="col-md-8">
                <div class="card-body">
                    <h5 class="card-title">{{ listing.Address }}</h5>
                    <p class="card-text">{{ listing.City }}, {{ listing.State }} {{ listing.Zip }}</p>
                    <p class="card-text">{{ listing.Country }}</p>
                    <p class="card-text"><small class="text-body-secondary">Last updated {{ listing.Updated }}</small></p>
                </div>
            </div>
            <div class="col">
                <div class="float-end">
                    <div class="btn-group" role="group" aria-label="Edit">
                        <a class="btn btn-secondary">Update</a>
                        <a class="btn btn-danger">Remove</a>
                        <a></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card mb-3">
        <div class="card-body">
            <h5 class="card-title">Activity</h5>
            <table>
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Who</th>
                        <th>What</th>
                        <th>Feedback</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(evt, index) in listing.Events" v-bind:key="index">
                        <td> {{ evt.DateTime }} </td>
                        <td> {{ evt.Person }} </td>
                        <td> {{ evt.Action }} </td>
                        <td> {{ evt.Feedback }} </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>
<script>
export default {
    name: 'PropertyListing',
    props: {
        listing: Object
    },
}
</script>

<style scoped></style>