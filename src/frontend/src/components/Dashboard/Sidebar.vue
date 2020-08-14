<template>
    <div class="sidebar-wrapper pt-16 h-screen min-h-screen flex items-start justify-start bg-gray-100 py-6">
		<div class="h-screen flex w-64 max-w-xs p-4 bg-gray-800">
			<ul class="flex flex-col w-full">
				<li class="my-px">
					<a href="#"
					   class="flex flex-row items-center h-12 px-4 rounded-lg text-gray-600 bg-gray-100">
						<span class="flex items-center justify-center text-lg text-gray-500">
							<svg fill="none"
								 stroke-linecap="round"
								 stroke-linejoin="round"
								 stroke-width="2"
								 viewBox="0 0 24 24"
								 stroke="currentColor"
								 class="h-6 w-6">
								<path d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6"></path>
							</svg>
						</span>
						<router-link to="/dashboard" class="ml-3">Dashboard</router-link>
						<!--<span class="flex items-center justify-center text-sm text-gray-500 font-semibold bg-gray-300 h-6 px-2 rounded-full ml-auto">3</span>-->
					</a>
				</li>
				<li class="my-px">
					<span class="flex font-medium text-sm text-gray-400 px-4 my-4 uppercase">Repositories</span>
				</li>
				<li class="my-px">
					<a href="#"
					   class="flex flex-row items-center h-12 px-4 rounded-lg text-gray-500 hover:bg-gray-700">
						<span class="flex items-center justify-center text-lg text-gray-500">
							<svg fill="none"
								 stroke-linecap="round"
								 stroke-linejoin="round"
								 stroke-width="2"
								 viewBox="0 0 24 24"
								 stroke="currentColor"
								 class="h-6 w-6">
								<path d="M5 8h14M5 8a2 2 0 110-4h14a2 2 0 110 4M5 8v10a2 2 0 002 2h10a2 2 0 002-2V8m-9 4h4"></path>
							</svg>
						</span>
						<router-link to="/dashboard" class="ml-3">Browse</router-link>
					</a>
				</li>
				<li class="my-px">
					<a href="#"
					   class="flex flex-row items-center h-12 px-4 rounded-lg text-gray-500 hover:bg-gray-700">
						<span class="flex items-center justify-center text-lg text-green-400">
							<svg fill="none"
								 stroke-linecap="round"
								 stroke-linejoin="round"
								 stroke-width="2"
								 viewBox="0 0 24 24"
								 stroke="currentColor"
								 class="h-6 w-6">
								<path d="M12 9v3m0 0v3m0-3h3m-3 0H9m12 0a9 9 0 11-18 0 9 9 0 0118 0z"></path>
							</svg>
						</span>
						<router-link to="/dashboard/add" class="ml-3">Add new</router-link>
					</a>
				</li>
			</ul>
		</div>
	</div>
</template>

<style lang="scss" scoped>
.h-screen {
    height: calc(100vh - 4rem);
}

.sidebar-wrapper {
	font-family: system-ui,-apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
    line-height: 1.5;
}
</style>