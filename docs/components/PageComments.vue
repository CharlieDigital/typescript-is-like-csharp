<script setup lang="ts">
// See: https://aiktb.dev/blog/giscus-with-vitepress
import { useData } from 'vitepress'

const { title, isDark } = useData()
</script>

<template>
  <div :key="title" class="giscus" style="margin-top: 2rem;">
    <!--
    <component
      :is="'script'"
      src="https://utteranc.es/client.js"
      repo="CharlieDigital/typescript-is-like-csharp"
      issue-term="pathname"
      theme="preferred-color-scheme"
      crossorigin="anonymous"
      async
    />
    -->
    <component
      :is="'script'"
      src="https://giscus.app/client.js"
      data-repo="CharlieDigital/typescript-is-like-csharp"
      data-repo-id="R_kgDON_pfFQ"
      data-category="Comment"
      data-category-id="DIC_kwDON_pfFc4CnfNy"
      data-mapping="pathname"
      data-strict="0"
      data-reactions-enabled="1"
      data-emit-metadata="0"
      data-input-position="top"
      :data-theme=" isDark ? 'catppuccin_macchiato' : 'light'"
      data-lang="en"
      crossorigin="anonymous"
      async
    />
  </div>
</template>
