<script setup lang="ts">
import DefaultTheme from 'vitepress/theme'

import PageComments from './components/PageComments.vue'

const { Layout } = DefaultTheme
</script>

<template>
  <Layout>
    <template #doc-after>
      <PageComments />
    </template>
  </Layout>
</template>
