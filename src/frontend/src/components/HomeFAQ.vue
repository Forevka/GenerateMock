<template>
    <div class="faq-wrapper space-y-5">
        <section class="how-to">
            <h2 class="font-bold text-4xl text-left pt-5">How to start?</h2>
            <ul class="list-decimal list-inside block text-1xl text-left pl-4 space-y-1">
                <li>Create repository on GitHub (In future i will extend list of database origin)</li>
                <li>Register in system, not necessarily with login from github.
                    You can add/browse any repositories</li>
                <li>Add repository with any .json file and remember them version(If it was first time then version would be <code class="code font-bold">v1</code>)</li>
                <li>Make request to <a href="#" class="text-blue-700 underline">http://mysite.com/<b>&lt;login&gt;</b>/<b>&lt;repo&gt;</b>/<b>&lt;version&gt;</b>/<b>&lt;db&gt;</b></a> and you will get json schema</li>
                <li>Make request to <a href="#" class="text-blue-700 underline">http://mysite.com/<b>&lt;login&gt;</b>/<b>&lt;repo&gt;</b>/<b>&lt;version&gt;</b>/<b>&lt;db&gt;</b>/<b>&lt;objectName&gt;</b></a> and you will get object from your json file!</li>
                <li>Enjoy!</li>
            </ul>
        </section>
        <section class="Example of usage">
            <h2 class="font-bold text-4xl text-left">Example of usage</h2>
            <div class="pl-4 text-left">You can check example database:</div>
            <div class="pl-4 text-left"><a href="#" class="text-blue-700 underline">http://mysite.com/forevka/demo/v1/posts/</a></div>
        </section>
        <section class="versioning">
            <h2 class="font-bold text-4xl text-left">About versioning</h2>
            <div class="pl-4 text-left">All databases that added to this system have unique version.</div>
            <div class="pl-4 text-left">If you want to update you json you also need to update them in this system</div>
            <div class="pl-4 text-left">This feature was introduced for better experience in use for teams.</div>
            <div class="pl-2 text-left font-bold">Quick example</div>
            <div class="pl-4 text-left">Your friend changed schema of some object but forget to tell you about that.</div>
            <div class="pl-4 text-left">So without versioning your services will fall down with validation errors etc.</div>
            <div class="pl-4 text-left">But when we have versioning you simply call to  <code class="code font-bold">v1</code> version of database, and your friend will call  <code class="code font-bold">v2</code></div>
            <div class="pl-4 text-left">No exceptions, no missunderstaning, you will update your services when you get know about new version</div>
        </section>
    </div>
</template>

<style lang="scss" scoped>
.faq-wrapper {
    width: 50%;
    left: 30%;
    position: absolute;
    height: 70%;
}

.code {
    background: #f4f5f6;
    border-radius: .4rem;
    font-size: 86%;
    margin: 0 .2rem;
    padding: .2rem .5rem;
    white-space: nowrap;
}

</style>