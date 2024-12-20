"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[454],{4983:(e,n,a)=>{a.r(n),a.d(n,{assets:()=>o,contentTitle:()=>c,default:()=>d,frontMatter:()=>i,metadata:()=>s,toc:()=>r});const s=JSON.parse('{"id":"expectations/collections","title":"Collections","description":"Describes the possible expectations for collections.","source":"@site/docs/expectations/collections.md","sourceDirName":"expectations","slug":"/expectations/collections","permalink":"/aweXpect/docs/expectations/collections","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/collections.md","tags":[],"version":"current","sidebarPosition":13,"frontMatter":{"sidebar_position":13},"sidebar":"tutorialSidebar","previous":{"title":"HTTP","permalink":"/aweXpect/docs/expectations/http"},"next":{"title":"Delegates","permalink":"/aweXpect/docs/expectations/delegates"}}');var t=a(4848),l=a(8453);const i={sidebar_position:13},c="Collections",o={},r=[{value:"Be",id:"be",level:2},{value:"All be",id:"all-be",level:2},{value:"All be unique",id:"all-be-unique",level:2},{value:"All satisfy",id:"all-satisfy",level:2},{value:"Sort order",id:"sort-order",level:2},{value:"Contain",id:"contain",level:2},{value:"Predicate",id:"predicate",level:3},{value:"Contain subset",id:"contain-subset",level:3},{value:"Be contained in",id:"be-contained-in",level:3},{value:"Start with",id:"start-with",level:2},{value:"Have",id:"have",level:2},{value:"All",id:"all",level:3},{value:"At least",id:"at-least",level:3},{value:"At most",id:"at-most",level:3},{value:"Between",id:"between",level:3},{value:"Exactly",id:"exactly",level:3},{value:"None",id:"none",level:3},{value:"Have single",id:"have-single",level:2},{value:"Dictionaries",id:"dictionaries",level:2},{value:"Contain key(s)",id:"contain-keys",level:3},{value:"Contain value(s)",id:"contain-values",level:3}];function h(e){const n={a:"a",code:"code",em:"em",h1:"h1",h2:"h2",h3:"h3",header:"header",p:"p",pre:"pre",...(0,l.R)(),...e.components};return(0,t.jsxs)(t.Fragment,{children:[(0,t.jsx)(n.header,{children:(0,t.jsx)(n.h1,{id:"collections",children:"Collections"})}),"\n",(0,t.jsx)(n.p,{children:"Describes the possible expectations for collections."}),"\n",(0,t.jsx)(n.h2,{id:"be",children:"Be"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that a collection matches another collection:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).Should().Be([1, 2, 3]);\nawait Expect.That(values).Should().Be([3, 2, 1]).InAnyOrder();\nawait Expect.That(values).Should().Be([1, 1, 2, 2, 3, 3]).IgnoringDuplicates();\nawait Expect.That(values).Should().Be([3, 3, 2, 2, 1, 1]).InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h2,{id:"all-be",children:"All be"}),"\n",(0,t.jsxs)(n.p,{children:["You can verify, that all items in the collection are equal to the ",(0,t.jsx)(n.code,{children:"expected"})," value"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"await Expect.That([1, 1, 1]).Should().AllBe(1);\n"})}),"\n",(0,t.jsxs)(n.p,{children:["You can also use a ",(0,t.jsx)(n.a,{href:"/docs/expectations/object#custom-comparer",children:"custom comparer"})," or configure ",(0,t.jsx)(n.a,{href:"/docs/expectations/object#equivalence",children:"equivalence"}),":"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<MyClass> values = //...\nMyClass expected = //...\n\nawait Expect.That(values).Should().AllBe(expected).Equivalent();\nawait Expect.That(values).Should().AllBe(expected).Using(new MyClassComparer());\n"})}),"\n",(0,t.jsxs)(n.p,{children:["For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing white-space, or use a custom ",(0,t.jsx)(n.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:'await Expect.That(["foo", "FOO", "Foo"]).Should().AllBe("foo").IgnoringCase();\n'})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h2,{id:"all-be-unique",children:"All be unique"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that all items in a collection are unique."}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"await Expect.That([1, 2, 3]).Should().AllBeUnique();\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.p,{children:"For dictionaries, this expectation only verifies the values, as the keys are unique by design:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IDictionary<int, int> subject = new Dictionary<int, int>\n{\n  { 1, 1 },\n  { 2, 1 }\n};\n\n// This following expectation will fail, even though the keys are unique!\nawait Expect.That(subject).Should().AllBeUnique();\n"})}),"\n",(0,t.jsx)(n.h2,{id:"all-satisfy",children:"All satisfy"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that all items in a collection satisfy a condition:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"await Expect.That([1, 2, 3]).Should().AllSatisfy(x => x < 4);\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h2,{id:"sort-order",children:"Sort order"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that the collection contains is sorted in ascending or descending order:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:'await Expect.That([1, 2, 3]).Should().BeInAscendingOrder();\nawait Expect.That(["c", "b", "a"]).Should().BeInDescendingOrder();\n'})}),"\n",(0,t.jsx)(n.p,{children:"You can also specify a custom comparer:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:'await Expect.That(["a", "B", "c"]).Should().BeInAscendingOrder().Using(StringComparer.OrdinalIgnoreCase);\n'})}),"\n",(0,t.jsx)(n.p,{children:"For objects, you can also verify the sort order on a member:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"MyClass[] values = //...\n\nawait Expect.That(values).Should().BeInAscendingOrder(x => x.Value);\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h2,{id:"contain",children:"Contain"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that the collection contains a specific item or not:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().Contain(13);\nawait Expect.That(values).Should().NotContain(42);\n"})}),"\n",(0,t.jsxs)(n.p,{children:["You can also set occurrence constraints on ",(0,t.jsx)(n.code,{children:"Contain"}),":"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [1, 1, 1, 2];\n\nawait Expect.That(values).Should().Contain(1).AtLeast(2.Times());\nawait Expect.That(values).Should().Contain(1).Exactly(3.Times());\nawait Expect.That(values).Should().Contain(1).AtMost(4.Times());\nawait Expect.That(values).Should().Contain(1).Between(1).And(5.Times());\n"})}),"\n",(0,t.jsxs)(n.p,{children:["You can also use a ",(0,t.jsx)(n.a,{href:"/docs/expectations/object#custom-comparer",children:"custom comparer"})," or configure ",(0,t.jsx)(n.a,{href:"/docs/expectations/object#equivalence",children:"equivalence"}),":"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<MyClass> values = //...\nMyClass expected = //...\n\nawait Expect.That(values).Should().Contain(expected).Equivalent();\nawait Expect.That(values).Should().Contain(expected).Using(new MyClassComparer());\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h3,{id:"predicate",children:"Predicate"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that the collection contains an item that satisfies a condition:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().Contain(x => x > 12 && x < 14);\nawait Expect.That(values).Should().NotContain(x => x >= 42);\n"})}),"\n",(0,t.jsxs)(n.p,{children:["You can also set occurrence constraints on ",(0,t.jsx)(n.code,{children:"Contain"}),":"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [1, 1, 1, 2];\n\nawait Expect.That(values).Should().Contain(x => x == 1).AtLeast(2.Times());\nawait Expect.That(values).Should().Contain(x => x == 1).Exactly(3.Times());\nawait Expect.That(values).Should().Contain(x => x == 1).AtMost(4.Times());\nawait Expect.That(values).Should().Contain(x => x == 1).Between(1).And(5.Times());\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h3,{id:"contain-subset",children:"Contain subset"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that a collection contains another collection as a subset:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).Should().Contain([1, 2]);\nawait Expect.That(values).Should().Contain([3, 2]).InAnyOrder();\nawait Expect.That(values).Should().Contain([1, 1, 2, 2]).IgnoringDuplicates();\nawait Expect.That(values).Should().Contain([3, 3, 1, 1]).InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsxs)(n.p,{children:["To check for a proper subset, append ",(0,t.jsx)(n.code,{children:".Properly()"})," (which would fail for equal collections)."]}),"\n",(0,t.jsx)(n.h3,{id:"be-contained-in",children:"Be contained in"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that a collection is contained in another collection (it is a superset):"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).Should().BeContainedIn([1, 2, 3, 4]);\nawait Expect.That(values).Should().BeContainedIn([4, 3, 2, 1]).InAnyOrder();\nawait Expect.That(values).Should().BeContainedIn([1, 1, 2, 2, 3, 3, 4, 4]).IgnoringDuplicates();\nawait Expect.That(values).Should().BeContainedIn([4, 4, 3, 3, 2, 2, 1, 1]).InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsxs)(n.p,{children:["To check for a proper superset, append ",(0,t.jsx)(n.code,{children:".Properly()"})," (which would fail for equal collections)."]}),"\n",(0,t.jsx)(n.h2,{id:"start-with",children:"Start with"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that a collection starts with another collection:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).Should().StartWith(1, 2);\n"})}),"\n",(0,t.jsxs)(n.p,{children:["You can also use a ",(0,t.jsx)(n.a,{href:"/docs/expectations/object#custom-comparer",children:"custom comparer"})," or configure ",(0,t.jsx)(n.a,{href:"/docs/expectations/object#equivalence",children:"equivalence"}),":"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<MyClass> values = //...\nMyClass expected = //...\n\nawait Expect.That(values).Should().StartWith(expected).Equivalent();\nawait Expect.That(values).Should().StartWith(expected).Using(new MyClassComparer());\n"})}),"\n",(0,t.jsxs)(n.p,{children:["For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing white-space, or use a custom ",(0,t.jsx)(n.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:'await Expect.That(["FOO", "BAR"]).Should().StartWith(["foo"]).IgnoringCase();\n'})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h2,{id:"have",children:"Have"}),"\n",(0,t.jsx)(n.p,{children:"Specifications that count the elements in a collection."}),"\n",(0,t.jsx)(n.h3,{id:"all",children:"All"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that all items in the collection, satisfy an expectation:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveAll(x => x.Satisfy(i => i <= 20));\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h3,{id:"at-least",children:"At least"}),"\n",(0,t.jsxs)(n.p,{children:["You can verify, that at least ",(0,t.jsx)(n.code,{children:"minimum"})," items in the collection, satisfy an expectation:"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveAtLeast(9, x => x.Satisfy(i => i < 10));\n"})}),"\n",(0,t.jsxs)(n.p,{children:["You can also verify, that the collection has at least ",(0,t.jsx)(n.code,{children:"minimum"})," items:"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Should().HaveAtLeast(9).Items();\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h3,{id:"at-most",children:"At most"}),"\n",(0,t.jsxs)(n.p,{children:["You can verify, that at most ",(0,t.jsx)(n.code,{children:"maximum"})," items in the collection, satisfy an expectation:"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveAtMost(1, x => x.Satisfy(i => i < 2));\n"})}),"\n",(0,t.jsxs)(n.p,{children:["You can also verify, that the collection has at most ",(0,t.jsx)(n.code,{children:"maximum"})," items:"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Should().HaveAtMost(11).Items();\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h3,{id:"between",children:"Between"}),"\n",(0,t.jsxs)(n.p,{children:["You can verify, that between ",(0,t.jsx)(n.code,{children:"minimum"})," and ",(0,t.jsx)(n.code,{children:"maximum"})," items in the collection, satisfy an expectation:"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveBetween(1).And(2, x => x.Satisfy(i => i < 2));\n"})}),"\n",(0,t.jsxs)(n.p,{children:["You can also verify, that the collection has between ",(0,t.jsx)(n.code,{children:"minimum"})," and ",(0,t.jsx)(n.code,{children:"maximum"})," items:"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Should().HaveBetween(9).And(11).Items();\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h3,{id:"exactly",children:"Exactly"}),"\n",(0,t.jsxs)(n.p,{children:["You can verify, that exactly ",(0,t.jsx)(n.code,{children:"expected"})," items in the collection, satisfy an expectation:"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveExactly(9, x => x.Satisfy(i => i < 10));\n"})}),"\n",(0,t.jsxs)(n.p,{children:["You can also verify, that the collection has exactly ",(0,t.jsx)(n.code,{children:"expected"})," items:"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Should().HaveExactly(10).Items();\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h3,{id:"none",children:"None"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that not item in the collection, satisfies an expectation:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Should().HaveNone(x => x.Satisfy(i => i > 20));\n"})}),"\n",(0,t.jsx)(n.p,{children:"You can also verify, that the collection is empty."}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Array.Empty<int>();\n\nawait Expect.That(values).Should().BeEmpty();\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h2,{id:"have-single",children:"Have single"}),"\n",(0,t.jsx)(n.p,{children:"You can verify, that the collection contains a single element that satisfies an expectation."}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [42];\n\nawait Expect.That(values).Should().HaveSingle();\nawait Expect.That(values).Should().HaveSingle().Which.Should().BeGreaterThan(41);\n"})}),"\n",(0,t.jsx)(n.p,{children:"The awaited result is the single element:"}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [42];\n\nint result = await Expect.That(values).Should().HaveSingle();\nawait Expect.That(result).Should().BeGreaterThan(41);\n"})}),"\n",(0,t.jsx)(n.p,{children:(0,t.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,t.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,t.jsx)(n.h2,{id:"dictionaries",children:"Dictionaries"}),"\n",(0,t.jsx)(n.h3,{id:"contain-keys",children:"Contain key(s)"}),"\n",(0,t.jsxs)(n.p,{children:["You can verify, that a dictionary contains the ",(0,t.jsx)(n.code,{children:"expected"})," key(s):"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:'Dictionary<int, string> values = new() { { 42, "foo" }, { 43, "bar" } };\n\nawait Expect.That(values).Should().ContainKey(42);\nawait Expect.That(values).Should().ContainKeys(42, 43);\nawait Expect.That(values).Should().NotContainKey(44);\nawait Expect.That(values).Should().NotContainKeys(44, 45, 46);\n'})}),"\n",(0,t.jsx)(n.h3,{id:"contain-values",children:"Contain value(s)"}),"\n",(0,t.jsxs)(n.p,{children:["You can verify, that a dictionary contains the ",(0,t.jsx)(n.code,{children:"expected"})," value(s):"]}),"\n",(0,t.jsx)(n.pre,{children:(0,t.jsx)(n.code,{className:"language-csharp",children:'Dictionary<int, string> values = new() { { 42, "foo" }, { 43, "bar" } };\n\nawait Expect.That(values).Should().ContainValue("foo");\nawait Expect.That(values).Should().ContainValues("foo", "bar");\nawait Expect.That(values).Should().NotContainValue("something");\nawait Expect.That(values).Should().NotContainValues("something", "else");\n'})})]})}function d(e={}){const{wrapper:n}={...(0,l.R)(),...e.components};return n?(0,t.jsx)(n,{...e,children:(0,t.jsx)(h,{...e})}):h(e)}},8453:(e,n,a)=>{a.d(n,{R:()=>i,x:()=>c});var s=a(6540);const t={},l=s.createContext(t);function i(e){const n=s.useContext(l);return s.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function c(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(t):e.components||t:i(e.components),s.createElement(l.Provider,{value:n},e.children)}}}]);