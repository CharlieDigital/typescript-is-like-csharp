// https://vitepress.dev/guide/custom-theme
import { h } from 'vue'
import type { Theme } from 'vitepress'
import DefaultTheme from 'vitepress/theme'
import './style.css'
import Vue3TouchEvents, { Vue3TouchEventsOptions } from 'vue3-touch-events';
import CodeSplitter from '../../components/CodeSplitter.vue'

export default {
  extends: DefaultTheme,
  Layout: () => {
    return h(DefaultTheme.Layout, null, {
      // https://vitepress.dev/guide/extending-default-theme#layout-slots
    })
  },
  enhanceApp({ app, router, siteData }) {
    app.component('CodeSplitter', CodeSplitter);
    app.use<Vue3TouchEventsOptions>(Vue3TouchEvents, {
      disableClick: false
    })
  }
} satisfies Theme
