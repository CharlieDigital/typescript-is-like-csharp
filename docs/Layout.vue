<script setup lang="ts">
import DefaultTheme from 'vitepress/theme'

import Comments from './components/UtterancesComments.vue'

const { Layout } = DefaultTheme
</script>

<template>
  <Layout>
    <template #doc-after>
      <Comments />
    </template>
  </Layout>
</template>
