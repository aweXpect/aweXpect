"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[454],{4983:(e,n,a)=>{a.r(n),a.d(n,{assets:()=>o,contentTitle:()=>i,default:()=>d,frontMatter:()=>c,metadata:()=>s,toc:()=>r});const s=JSON.parse('{"id":"expectations/collections","title":"Collections","description":"Describes the possible expectations for collections.","source":"@site/docs/expectations/collections.md","sourceDirName":"expectations","slug":"/expectations/collections","permalink":"/aweXpect/docs/expectations/collections","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/collections.md","tags":[],"version":"current","sidebarPosition":13,"frontMatter":{"sidebar_position":13},"sidebar":"tutorialSidebar","previous":{"title":"HTTP","permalink":"/aweXpect/docs/expectations/http"},"next":{"title":"Delegates","permalink":"/aweXpect/docs/expectations/delegates"}}');var l=a(4848),t=a(8453);const c={sidebar_position:13},i="Collections",o={},r=[{value:"Be",id:"be",level:2},{value:"Subset",id:"subset",level:3},{value:"Superset",id:"superset",level:3},{value:"All be",id:"all-be",level:2},{value:"All be unique",id:"all-be-unique",level:2},{value:"All satisfy",id:"all-satisfy",level:2},{value:"Contain",id:"contain",level:2},{value:"Predicate",id:"predicate",level:3},{value:"Have",id:"have",level:2},{value:"All",id:"all",level:3},{value:"At least",id:"at-least",level:3},{value:"At most",id:"at-most",level:3},{value:"Between",id:"between",level:3},{value:"Exactly",id:"exactly",level:3},{value:"None",id:"none",level:3},{value:"Have single",id:"have-single",level:2}];function h(e){const n={a:"a",code:"code",em:"em",h1:"h1",h2:"h2",h3:"h3",header:"header",p:"p",pre:"pre",...(0,t.R)(),...e.components};return(0,l.jsxs)(l.Fragment,{children:[(0,l.jsx)(n.header,{children:(0,l.jsx)(n.h1,{id:"collections",children:"Collections"})}),"\n",(0,l.jsx)(n.p,{children:"Describes the possible expectations for collections."}),"\n",(0,l.jsx)(n.h2,{id:"be",children:"Be"}),"\n",(0,l.jsx)(n.p,{children:"You can verify, that a collection matches another collection:"}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).Should().Be([1, 2, 3]);\nawait Expect.That(values).Should().Be([3, 2, 1]).InAnyOrder();\nawait Expect.That(values).Should().Be([1, 1, 2, 2, 3, 3]).IgnoringDuplicates();\nawait Expect.That(values).Should().Be([3, 3, 2, 2, 1, 1]).InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h3,{id:"subset",children:"Subset"}),"\n",(0,l.jsx)(n.p,{children:"You can verify, that a collection matches another collection or has fewer items (it is a subset of the expected items):"}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).Should().Be([1, 2, 3, 4]).OrLess();\nawait Expect.That(values).Should().Be([4, 3, 2, 1]).OrLess().InAnyOrder();\nawait Expect.That(values).Should().Be([1, 1, 2, 2, 3, 3, 4, 4]).OrLess().IgnoringDuplicates();\nawait Expect.That(values).Should().Be([4, 4, 3, 3, 2, 2, 1, 1]).OrLess().InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsxs)(n.p,{children:["To check for a proper subset, use ",(0,l.jsx)(n.code,{children:"AndLess"})," instead (which would fail for equal collections)."]}),"\n",(0,l.jsx)(n.h3,{id:"superset",children:"Superset"}),"\n",(0,l.jsx)(n.p,{children:"You can verify, that a collection matches another collection or has more items (it is a superset of the expected items):"}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).Should().Be([1, 2]).OrMore();\nawait Expect.That(values).Should().Be([3, 2]).OrMore().InAnyOrder();\nawait Expect.That(values).Should().Be([1, 1, 2, 2]).OrMore().IgnoringDuplicates();\nawait Expect.That(values).Should().Be([3, 3, 1, 1]).OrMore().InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsxs)(n.p,{children:["To check for a proper superset, use ",(0,l.jsx)(n.code,{children:"AndMore"})," instead (which would fail for equal collections)."]}),"\n",(0,l.jsx)(n.h2,{id:"all-be",children:"All be"}),"\n",(0,l.jsxs)(n.p,{children:["You can verify, that all items in the collection are equal to the ",(0,l.jsx)(n.code,{children:"expected"})," value"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"int[] values = [1, 1, 1]\n\nawait Expect.That(values).Should().AllBe(1);\n"})}),"\n",(0,l.jsxs)(n.p,{children:["You can also use a ",(0,l.jsx)(n.a,{href:"/docs/expectations/object#custom-comparer",children:"custom comparer"})," or configure ",(0,l.jsx)(n.a,{href:"/docs/expectations/object#equivalence",children:"equivalence"}),":"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<MyClass> values = //...\nMyClass expected = //...\n\nawait Expect.That(values).Should().AllBe(expected).Equivalent();\nawait Expect.That(values).Should().AllBe(expected).Using(new MyClassComparer());\n"})}),"\n",(0,l.jsxs)(n.p,{children:["For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing white-space, or use a custom ",(0,l.jsx)(n.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:'string[] values = ["foo", "FOO", "Foo"];\n\nawait Expect.That(values).Should().AllBe("foo").IgnoringCase();\n'})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h2,{id:"all-be-unique",children:"All be unique"}),"\n",(0,l.jsx)(n.p,{children:"You can verify, that all items in a collection are unique."}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [1, 2, 3];\n\nawait Expect.That(values).Should().AllBeUnique();\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h2,{id:"all-satisfy",children:"All satisfy"}),"\n",(0,l.jsx)(n.p,{children:"You can verify, that all items in a collection satisfy a condition:"}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"int[] values = [1, 2, 3];\n\nawait Expect.That(values).Should().AllSatisfy(x => x < 4);\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h2,{id:"contain",children:"Contain"}),"\n",(0,l.jsx)(n.p,{children:"You can verify, that the collection contains a specific item or not:"}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().Contain(13);\nawait Expect.That(values).Should().NotContain(42);\n"})}),"\n",(0,l.jsxs)(n.p,{children:["You can also set occurrence constraints on ",(0,l.jsx)(n.code,{children:"Contain"}),":"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [1, 1, 1, 2];\n\nawait Expect.That(values).Should().Contain(1).AtLeast(2.Times());\nawait Expect.That(values).Should().Contain(1).Exactly(3.Times());\nawait Expect.That(values).Should().Contain(1).AtMost(4.Times());\nawait Expect.That(values).Should().Contain(1).Between(1).And(5.Times());\n"})}),"\n",(0,l.jsxs)(n.p,{children:["You can also use a ",(0,l.jsx)(n.a,{href:"/docs/expectations/object#custom-comparer",children:"custom comparer"})," or configure ",(0,l.jsx)(n.a,{href:"/docs/expectations/object#equivalence",children:"equivalence"}),":"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<MyClass> values = //...\nMyClass expected = //...\n\nawait Expect.That(values).Should().Contain(expected).Equivalent();\nawait Expect.That(values).Should().Contain(expected).Using(new MyClassComparer());\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h3,{id:"predicate",children:"Predicate"}),"\n",(0,l.jsx)(n.p,{children:"You can verify, that the collection contains an item that satisfies a condition:"}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().Contain(x => x > 12 && x < 14);\nawait Expect.That(values).Should().NotContain(x => x >= 42);\n"})}),"\n",(0,l.jsxs)(n.p,{children:["You can also set occurrence constraints on ",(0,l.jsx)(n.code,{children:"Contain"}),":"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [1, 1, 1, 2];\n\nawait Expect.That(values).Should().Contain(x => x == 1).AtLeast(2.Times());\nawait Expect.That(values).Should().Contain(x => x == 1).Exactly(3.Times());\nawait Expect.That(values).Should().Contain(x => x == 1).AtMost(4.Times());\nawait Expect.That(values).Should().Contain(x => x == 1).Between(1).And(5.Times());\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h2,{id:"have",children:"Have"}),"\n",(0,l.jsx)(n.p,{children:"Specifications that count the elements in a collection."}),"\n",(0,l.jsx)(n.h3,{id:"all",children:"All"}),"\n",(0,l.jsx)(n.p,{children:"You can verify, that all items in the collection, satisfy an expectation:"}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveAll(x => x.Satisfy(i => i <= 20));\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h3,{id:"at-least",children:"At least"}),"\n",(0,l.jsxs)(n.p,{children:["You can verify, that at least ",(0,l.jsx)(n.code,{children:"minimum"})," items in the collection, satisfy an expectation:"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveAtLeast(9, x => x.Satisfy(i => i < 10));\n"})}),"\n",(0,l.jsxs)(n.p,{children:["You can also verify, that the collection has at least ",(0,l.jsx)(n.code,{children:"minimum"})," items:"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Should().HaveAtLeast(9).Items();\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h3,{id:"at-most",children:"At most"}),"\n",(0,l.jsxs)(n.p,{children:["You can verify, that at most ",(0,l.jsx)(n.code,{children:"maximum"})," items in the collection, satisfy an expectation:"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveAtMost(1, x => x.Satisfy(i => i < 2));\n"})}),"\n",(0,l.jsxs)(n.p,{children:["You can also verify, that the collection has at most ",(0,l.jsx)(n.code,{children:"maximum"})," items:"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Should().HaveAtMost(11).Items();\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h3,{id:"between",children:"Between"}),"\n",(0,l.jsxs)(n.p,{children:["You can verify, that between ",(0,l.jsx)(n.code,{children:"minimum"})," and ",(0,l.jsx)(n.code,{children:"maximum"})," items in the collection, satisfy an expectation:"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\nawait Expect.That(values).Should().HaveBetween(1).And(2, x => x.Satisfy(i => i < 2));\n"})}),"\n",(0,l.jsxs)(n.p,{children:["You can also verify, that the collection has between ",(0,l.jsx)(n.code,{children:"minimum"})," and ",(0,l.jsx)(n.code,{children:"maximum"})," items:"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Should().HaveBetween(9).And(11).Items();\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h3,{id:"exactly",children:"Exactly"}),"\n",(0,l.jsxs)(n.p,{children:["You can verify, that exactly ",(0,l.jsx)(n.code,{children:"expected"})," items in the collection, satisfy an expectation:"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveExactly(9, x => x.Satisfy(i => i < 10));\n"})}),"\n",(0,l.jsxs)(n.p,{children:["You can also verify, that the collection has exactly ",(0,l.jsx)(n.code,{children:"expected"})," items:"]}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Should().HaveExactly(10).Items();\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h3,{id:"none",children:"None"}),"\n",(0,l.jsx)(n.p,{children:"You can verify, that not item in the collection, satisfies an expectation:"}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveNone(x => x.Satisfy(i => i > 20));\n"})}),"\n",(0,l.jsx)(n.p,{children:"You can also verify, that the collection is empty."}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Array.Empty<int>();\n\nawait Expect.That(values).Should().BeEmpty();\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,l.jsx)(n.h2,{id:"have-single",children:"Have single"}),"\n",(0,l.jsx)(n.p,{children:"You can verify, that the collection contains a single element that satisfies an expectation."}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [42];\n\nawait Expect.That(values).Should().HaveSingle();\nawait Expect.That(values).Should().HaveSingle().Which.Should().BeGreaterThan(41);\n"})}),"\n",(0,l.jsx)(n.p,{children:"The awaited result is the single element:"}),"\n",(0,l.jsx)(n.pre,{children:(0,l.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [42];\n\nint result = await Expect.That(values).Should().HaveSingle();\nawait Expect.That(result).Should().BeGreaterThan(41);\n"})}),"\n",(0,l.jsx)(n.p,{children:(0,l.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,l.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})})]})}function d(e={}){const{wrapper:n}={...(0,t.R)(),...e.components};return n?(0,l.jsx)(n,{...e,children:(0,l.jsx)(h,{...e})}):h(e)}},8453:(e,n,a)=>{a.d(n,{R:()=>c,x:()=>i});var s=a(6540);const l={},t=s.createContext(l);function c(e){const n=s.useContext(t);return s.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function i(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(l):e.components||l:c(e.components),s.createElement(t.Provider,{value:n},e.children)}}}]);