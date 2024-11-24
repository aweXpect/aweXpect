"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[924],{6475:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>c,contentTitle:()=>o,default:()=>u,frontMatter:()=>i,metadata:()=>a,toc:()=>d});const a=JSON.parse('{"id":"getting-started","title":"Getting started","description":"Installation","source":"@site/docs/getting-started.md","sourceDirName":".","slug":"/getting-started","permalink":"/aweXpect/docs/getting-started","draft":false,"unlisted":false,"editUrl":"https://github.com/facebook/docusaurus/tree/main/packages/create-docusaurus/templates/shared/docs/getting-started.md","tags":[],"version":"current","sidebarPosition":1,"frontMatter":{"sidebar_position":1},"sidebar":"tutorialSidebar","next":{"title":"Basics","permalink":"/aweXpect/docs/category/basics"}}');var s=n(4848),r=n(8453);const i={sidebar_position:1},o="Getting started",c={},d=[{value:"Installation",id:"installation",level:2},{value:"Write your first expectation",id:"write-your-first-expectation",level:2}];function l(e){const t={a:"a",code:"code",h1:"h1",h2:"h2",header:"header",p:"p",pre:"pre",...(0,r.R)(),...e.components};return(0,s.jsxs)(s.Fragment,{children:[(0,s.jsx)(t.header,{children:(0,s.jsx)(t.h1,{id:"getting-started",children:"Getting started"})}),"\n",(0,s.jsx)(t.h2,{id:"installation",children:"Installation"}),"\n",(0,s.jsxs)(t.p,{children:["Install the ",(0,s.jsx)(t.a,{href:"https://www.nuget.org/packages/aweXpect",children:(0,s.jsx)(t.code,{children:"aweXpect"})})," nuget package"]}),"\n",(0,s.jsx)(t.pre,{children:(0,s.jsx)(t.code,{className:"language-ps",children:"dotnet add package aweXpect\n"})}),"\n",(0,s.jsxs)(t.p,{children:["Optional: If you want to simplify the assertions, you can add a ",(0,s.jsx)(t.code,{children:"global using static aweXpect.Expect;"})," statement anywhere in your test project."]}),"\n",(0,s.jsx)(t.h2,{id:"write-your-first-expectation",children:"Write your first expectation"}),"\n",(0,s.jsx)(t.p,{children:"Write your first expectation:"}),"\n",(0,s.jsx)(t.pre,{children:(0,s.jsx)(t.code,{className:"language-csharp",children:'[Fact]\npublic async Task SomeMethod_ShouldThrowArgumentNullException()\n{\n  await That(SomeMethod).Should().Throw<ArgumentNullException>()\n    .WithMessage("Value cannot be null")\n    .Because("we tested the null edge case");\n}\n'})})]})}function u(e={}){const{wrapper:t}={...(0,r.R)(),...e.components};return t?(0,s.jsx)(t,{...e,children:(0,s.jsx)(l,{...e})}):l(e)}},8453:(e,t,n)=>{n.d(t,{R:()=>i,x:()=>o});var a=n(6540);const s={},r=a.createContext(s);function i(e){const t=a.useContext(r);return a.useMemo((function(){return"function"==typeof e?e(t):{...t,...e}}),[t,e])}function o(e){let t;return t=e.disableParentContext?"function"==typeof e.components?e.components(s):e.components||s:i(e.components),a.createElement(r.Provider,{value:t},e.children)}}}]);