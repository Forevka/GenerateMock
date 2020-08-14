<template>
    <div class="faq-wrapper space-y-5">
        <div class="pt-6 font-bold text-4xl">
            <span>FAQ</span>
            <div class="w-full mb-4">
            <div
                class="h-1 mx-auto gradient w-64 opacity-25 my-0 py-0 rounded-t"
            ></div>
            </div>
        </div>
        <section class="container mx-auto pt-5 px-4 md:px-2">
            <div class="flex items-start justify-start mb-12">
                <svg fill="none" viewBox="0 0 24 24" stroke="currentColor" class="mr-4 text-gray-700 w-6 h-6 flex-none" aria-hidden="true">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8.228 9c.549-1.165 2.03-2 3.772-2 2.21 0 4 1.343 4 3 0 1.4-1.278 2.575-3.006 2.907-.542.104-.994.54-.994 1.093m0 3h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
                </svg>
                <div>
                    <p class="font-semibold mt-0 mb-3 text-gray-900 text-left">How to start?</p>
                    <ul class="list-decimal list-inside block text-1xl text-left space-y-1">
                        <li>Create repository on GitHub (In future i will extend list of database origin)</li>
                        <li>Register in system, not necessarily with login from github.
                            You can add/browse any repositories</li>
                        <li>Add repository with any .json file and remember them version(If it was first time then version would be <code class="code font-bold">v1</code>)</li>
                        <li>Make request to <a href="#" class="text-blue-700 underline">http://mysite.com/<b>&lt;login&gt;</b>/<b>&lt;repo&gt;</b>/<b>&lt;version&gt;</b>/<b>&lt;db&gt;</b></a> and you will get json schema</li>
                        <li>Make request to <a href="#" class="text-blue-700 underline">http://mysite.com/<b>&lt;login&gt;</b>/<b>&lt;repo&gt;</b>/<b>&lt;version&gt;</b>/<b>&lt;db&gt;</b>/<b>&lt;objectName&gt;</b></a> and you will get object from your json file!</li>
                        <li>Enjoy!</li>
                    </ul>
                </div>
            </div>
            <div class="flex items-start justify-start mb-12">
                <svg fill="none" viewBox="0 0 24 24" stroke="currentColor" class="mr-4 text-gray-700 w-6 h-6 flex-none" aria-hidden="true">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8.228 9c.549-1.165 2.03-2 3.772-2 2.21 0 4 1.343 4 3 0 1.4-1.278 2.575-3.006 2.907-.542.104-.994.54-.994 1.093m0 3h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
                </svg>
                <div>
                    <p class="font-semibold mt-0 mb-3 text-gray-900 text-left">Example of usage?</p>
                    <p class="text-gray-700 text-left">You can check example database:</p>
                    <p class="text-gray-700"><a href="#" class="text-blue-700 underline">http://mysite.com/forevka/demo/v1/posts/</a></p>
                </div>
            </div>
            <div class="flex items-start justify-start mb-12">
                <svg fill="none" viewBox="0 0 24 24" stroke="currentColor" class="mr-4 text-gray-700 w-6 h-6 flex-none" aria-hidden="true">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8.228 9c.549-1.165 2.03-2 3.772-2 2.21 0 4 1.343 4 3 0 1.4-1.278 2.575-3.006 2.907-.542.104-.994.54-.994 1.093m0 3h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
                </svg>
                <div>
                    <p class="font-semibold mt-0 mb-3 text-gray-900 text-left">Versioning?</p>
                    <p class="text-left">If you want to update you json you also need to update them in this system</p>
                    <p class="text-left">All databases that added to this system have unique version.</p>
                    <p class="text-left">This feature was introduced for better experience in use for teams.</p>
                    <p class="text-left font-bold">Quick example</p>
                    <p class="text-left">Your friend changed schema of some object but forget to tell you about that.</p>
                    <p class="text-left">So without versioning your services will fall down with validation errors etc.</p>
                    <p class="text-left">But when we have versioning you simply call to  <code class="code font-bold">v1</code> version of database, and your friend will call  <code class="code font-bold">v2</code></p>
                    <p class="text-left">No exceptions, no missunderstaning, you will update your services when you get know about new version</p>
                </div>
            </div>
        </section>
    </div>
    
</template>

<style lang="scss" scoped>
.faq-wrapper {
    margin: auto;
    width: 50%;
}

.code {
    background: #f4f5f6;
    border-radius: .4rem;
    font-size: 86%;
    margin: 0 .2rem;
    padding: .2rem .5rem;
    white-space: nowrap;
}

.gradient {
  background-image: linear-gradient(-225deg, #cbbacc 0%, #2580b3 100%);
}
</style>