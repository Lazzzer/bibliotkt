<template>
  <header class="bg-blue-600">
    <nav class="px-4 mx-auto max-w-7xl sm:px-6 lg:px-8" aria-label="Top">
      <div class="flex items-center justify-between w-full py-6 border-b border-blue-500 lg:border-none">
        <div class="flex items-center">
          <div class="hidden ml-10 space-x-8 lg:block">
            <router-link to="/" class="text-base font-medium text-white hover:text-blue-50">Home</router-link>
            <router-link to="/about" class="text-base font-medium text-white hover:text-blue-50">About</router-link>
          </div>
        </div>
        <div class="ml-10 space-x-4">
          <a href="#" class="inline-block px-4 py-2 text-base font-medium text-white bg-blue-500 border border-transparent rounded-md hover:bg-opacity-75">Log in</a>
        </div>
      </div>
      <div class="flex flex-wrap justify-center py-4 space-x-6 lg:hidden">
        <router-link to="/" class="text-base font-medium text-white hover:text-indigo-50">Home</router-link>
        <router-link to="/about" class="text-base font-medium text-white hover:text-indigo-50">About</router-link>
      </div>
    </nav>
  </header>
</template>