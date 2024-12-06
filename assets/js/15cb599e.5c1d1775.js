"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[454],{4983:(e,n,a)=>{a.r(n),a.d(n,{assets:()=>o,contentTitle:()=>i,default:()=>d,frontMatter:()=>c,metadata:()=>t,toc:()=>r});const t=JSON.parse('{"id":"expectations/collections","title":"Collections","description":"Describes the possible expectations for collections.","source":"@site/docs/expectations/collections.md","sourceDirName":"expectations","slug":"/expectations/collections","permalink":"/aweXpect/docs/expectations/collections","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/collections.md","tags":[],"version":"current","sidebarPosition":13,"frontMatter":{"sidebar_position":13},"sidebar":"tutorialSidebar","previous":{"title":"HTTP","permalink":"/aweXpect/docs/expectations/http"},"next":{"title":"Delegates","permalink":"/aweXpect/docs/expectations/delegates"}}');var s=a(4848),l=a(8453);const c={sidebar_position:13},i="Collections",o={},r=[{value:"Be",id:"be",level:2},{value:"Subset",id:"subset",level:3},{value:"Superset",id:"superset",level:3},{value:"Have",id:"have",level:2},{value:"All",id:"all",level:3},{value:"At least",id:"at-least",level:3},{value:"At most",id:"at-most",level:3},{value:"Between",id:"between",level:3},{value:"None",id:"none",level:3},{value:"Contain",id:"contain",level:2},{value:"Predicate",id:"predicate",level:2}];function h(e){const n={a:"a",code:"code",em:"em",h1:"h1",h2:"h2",h3:"h3",header:"header",p:"p",pre:"pre",...(0,l.R)(),...e.components};return(0,s.jsxs)(s.Fragment,{children:[(0,s.jsx)(n.header,{children:(0,s.jsx)(n.h1,{id:"collections",children:"Collections"})}),"\n",(0,s.jsx)(n.p,{children:"Describes the possible expectations for collections."}),"\n",(0,s.jsx)(n.h2,{id:"be",children:"Be"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that a collection matches another collection:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).Should().Be([1, 2, 3]);\nawait Expect.That(values).Should().Be([3, 2, 1]).InAnyOrder();\nawait Expect.That(values).Should().Be([1, 1, 2, 2, 3, 3]).IgnoringDuplicates();\nawait Expect.That(values).Should().Be([3, 3, 2, 2, 1, 1]).InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"subset",children:"Subset"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that a collection matches another collection or has fewer items (it is a subset of the expected items):"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).Should().Be([1, 2, 3, 4]).OrLess();\nawait Expect.That(values).Should().Be([4, 3, 2, 1]).OrLess().InAnyOrder();\nawait Expect.That(values).Should().Be([1, 1, 2, 2, 3, 3, 4, 4]).OrLess().IgnoringDuplicates();\nawait Expect.That(values).Should().Be([4, 4, 3, 3, 2, 2, 1, 1]).OrLess().InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsxs)(n.p,{children:["To check for a proper subset, use ",(0,s.jsx)(n.code,{children:"AndLess"})," instead (which would fail for equal collections)."]}),"\n",(0,s.jsx)(n.h3,{id:"superset",children:"Superset"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that a collection matches another collection or has more items (it is a superset of the expected items):"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).Should().Be([1, 2]).OrMore();\nawait Expect.That(values).Should().Be([3, 2]).OrMore().InAnyOrder();\nawait Expect.That(values).Should().Be([1, 1, 2, 2]).OrMore().IgnoringDuplicates();\nawait Expect.That(values).Should().Be([3, 3, 1, 1]).OrMore().InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsxs)(n.p,{children:["To check for a proper superset, use ",(0,s.jsx)(n.code,{children:"AndMore"})," instead (which would fail for equal collections)."]}),"\n",(0,s.jsx)(n.h2,{id:"have",children:"Have"}),"\n",(0,s.jsx)(n.p,{children:"Specifications that match the items on a given number of occurrences."}),"\n",(0,s.jsx)(n.h3,{id:"all",children:"All"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that all items in the collection, satisfy an expectation:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveAll(x => x.Satisfy(i => i <= 20));\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"at-least",children:"At least"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that at least a fixed number of items in the collection, satisfy an expectation:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveAtLeast(9, x => x.Satisfy(i => i < 10));\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"at-most",children:"At most"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that at most a fixed number of items in the collection, satisfy an expectation:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveAtMost(1, x => x.Satisfy(i => i < 2));\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"between",children:"Between"}),"\n",(0,s.jsxs)(n.p,{children:["You can verify, that between ",(0,s.jsx)(n.code,{children:"minimum"})," and ",(0,s.jsx)(n.code,{children:"maximum"})," items in the collection, satisfy an expectation:"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\nawait Expect.That(values).Should().HaveBetween(1).And(2, x => x.Satisfy(i => i < 2));\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"none",children:"None"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that not item in the collection, satisfies an expectation:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveNone(x => x.Satisfy(i => i > 20));\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h2,{id:"contain",children:"Contain"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that the collection contains a specific item or not:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().Contain(13);\nawait Expect.That(values).Should().NotContain(42);\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also set occurrence constraints on ",(0,s.jsx)(n.code,{children:"Contain"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [1, 1, 1, 2];\n\nawait Expect.That(values).Should().Contain(1).AtLeast(2.Times());\nawait Expect.That(values).Should().Contain(1).Exactly(3.Times());\nawait Expect.That(values).Should().Contain(1).AtMost(4.Times());\nawait Expect.That(values).Should().Contain(1).Between(1).And(5.Times());\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also use a ",(0,s.jsx)(n.a,{href:"/docs/expectations/object#custom-comparer",children:"custom comparer"})," or configure ",(0,s.jsx)(n.a,{href:"/docs/expectations/object#equivalence",children:"equivalence"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<MyClass> values = //...\nMyClass expected = //...\n\nawait Expect.That(values).Should().Contain(expected).Equivalent();\nawait Expect.That(values).Should().Contain(expected).Using(new MyClassComparer());\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h2,{id:"predicate",children:"Predicate"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that the collection contains an item that satisfies a condition:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().Contain(x => x > 12 && x < 14);\nawait Expect.That(values).Should().NotContain(x => x >= 42);\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also set occurrence constraints on ",(0,s.jsx)(n.code,{children:"Contain"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [1, 1, 1, 2];\n\nawait Expect.That(values).Should().Contain(x => x == 1).AtLeast(2.Times());\nawait Expect.That(values).Should().Contain(x => x == 1).Exactly(3.Times());\nawait Expect.That(values).Should().Contain(x => x == 1).AtMost(4.Times());\nawait Expect.That(values).Should().Contain(x => x == 1).Between(1).And(5.Times());\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})})]})}function d(e={}){const{wrapper:n}={...(0,l.R)(),...e.components};return n?(0,s.jsx)(n,{...e,children:(0,s.jsx)(h,{...e})}):h(e)}},8453:(e,n,a)=>{a.d(n,{R:()=>c,x:()=>i});var t=a(6540);const s={},l=t.createContext(s);function c(e){const n=t.useContext(l);return t.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function i(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(s):e.components||s:c(e.components),t.createElement(l.Provider,{value:n},e.children)}}}]);