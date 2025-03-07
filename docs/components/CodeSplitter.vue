<template>
  <div class="code-splitter container">
    <div ref="leftPanel" class="panel left" :style="{ width: `${leftWidth}%` }">
      <slot name="left"></slot>
    </div>
    <div
      ref="splitter"
      class="splitter"
      @mousedown="startResize"
      v-touch:press="startResize"
      v-touch:drag="onMouseMove">
    </div>
    <div ref="rightPanel" class="panel right" :style="{ width: `${rightWidth}%` }">
      <slot name="right"></slot>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";

const leftPanel = ref<HTMLDivElement>();
const rightPanel = ref<HTMLDivElement>();
const splitter = ref<HTMLDivElement>();

let isResizing = false;
const leftWidth = ref(50); // Default width of left panel
const rightWidth = ref(50); // Default width of right panel

const startResize = (event) => {
  isResizing = true;
  window.addEventListener("mousemove", onMouseMove);
  window.addEventListener("mouseup", onMouseUp);
  console.log("START RESIZE");
};

const onMouseMove = (e) => {
  if (!isResizing) return;

  const containerRect = splitter.value!.parentElement!.getBoundingClientRect();
  const offset = (e.clientX ?? e.touches[0].clientX) - containerRect.left;
  leftWidth.value = Math.max(5, (offset / containerRect.width) * 100); // Prevents collapsing completely
  rightWidth.value = Math.max(5, 100 - leftWidth.value); // Ensures the right panel adjusts accordingly;
};

const onMouseUp = () => {
  isResizing = false;
  window.removeEventListener("mousemove", onMouseMove);
  window.removeEventListener("mouseup", onMouseUp);
};
</script>

<style scoped>
.container {
  display: flex;
  flex-direction: row;
  overflow: hidden;
}

.panel {
  overflow: hidden;
  white-space: nowrap;
  display: flex;
}

.splitter {
  width: 5px;
  border-radius: 4px;
  opacity: 0.2;
  background: #666;
  cursor: ew-resize;
}

.left > :deep(div) {
  flex: 1;
  border-top-right-radius: 0px;
  border-bottom-right-radius: 0px;
}

.right > :deep(div) {
  flex: 1;
  border-top-left-radius: 0px;
  border-bottom-left-radius: 0px;
}

@media only screen and (min-width: 600px) {
  .panel :deep(code),
  .panel :deep(pre) {
    overflow: hidden !important;
  }
}
</style>
