<template>
    <h1>A single user, with ID: {{ $route.params.id }}</h1>
</template>

<script>
export default {
    validate (data) {
        return /^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$/.test(data.params.id)
    },
    layout: 'users'
}
</script>