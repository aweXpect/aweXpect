"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[4989],{3377:(e,n,a)=>{a.r(n),a.d(n,{assets:()=>r,contentTitle:()=>l,default:()=>d,frontMatter:()=>c,metadata:()=>t,toc:()=>o});const t=JSON.parse('{"id":"expectations/collections","title":"Collections","description":"Describes the possible expectations for collections.","source":"@site/docs/expectations/06-collections.md","sourceDirName":"expectations","slug":"/expectations/collections","permalink":"/docs/expectations/collections","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/06-collections.md","tags":[],"version":"current","sidebarPosition":6,"frontMatter":{},"sidebar":"documentationSidebar","previous":{"title":"Delegates","permalink":"/docs/expectations/delegates"},"next":{"title":"Object","permalink":"/docs/expectations/object"}}');var s=a(4848),i=a(8453);const c={},l="Collections",r={},o=[{value:"Equality",id:"equality",level:2},{value:"All be",id:"all-be",level:2},{value:"All be unique",id:"all-be-unique",level:2},{value:"Elements",id:"elements",level:2},{value:"Comply with",id:"comply-with",level:3},{value:"Satisfy",id:"satisfy",level:3},{value:"Sort order",id:"sort-order",level:2},{value:"Contain",id:"contain",level:2},{value:"Predicate",id:"predicate",level:3},{value:"Contain subset",id:"contain-subset",level:3},{value:"Be contained in",id:"be-contained-in",level:3},{value:"Start with",id:"start-with",level:2},{value:"End with",id:"end-with",level:2},{value:"Have",id:"have",level:2},{value:"All",id:"all",level:3},{value:"At least",id:"at-least",level:3},{value:"At most",id:"at-most",level:3},{value:"Between",id:"between",level:3},{value:"Exactly",id:"exactly",level:3},{value:"None",id:"none",level:3},{value:"Have single",id:"have-single",level:2},{value:"Dictionaries",id:"dictionaries",level:2},{value:"Contain key(s)",id:"contain-keys",level:3},{value:"Contain value(s)",id:"contain-values",level:3}];function h(e){const n={a:"a",code:"code",em:"em",h1:"h1",h2:"h2",h3:"h3",header:"header",p:"p",pre:"pre",...(0,i.R)(),...e.components};return(0,s.jsxs)(s.Fragment,{children:[(0,s.jsx)(n.header,{children:(0,s.jsx)(n.h1,{id:"collections",children:"Collections"})}),"\n",(0,s.jsx)(n.p,{children:"Describes the possible expectations for collections."}),"\n",(0,s.jsx)(n.h2,{id:"equality",children:"Equality"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that a collection matches another collection:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).IsEqualTo([1, 2, 3]);\nawait Expect.That(values).IsEqualTo([3, 2, 1]).InAnyOrder();\nawait Expect.That(values).IsEqualTo([1, 1, 2, 2, 3, 3]).IgnoringDuplicates();\nawait Expect.That(values).IsEqualTo([3, 3, 2, 2, 1, 1]).InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h2,{id:"all-be",children:"All be"}),"\n",(0,s.jsxs)(n.p,{children:["You can verify, that all items in the collection are equal to the ",(0,s.jsx)(n.code,{children:"expected"})," value"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"await Expect.That([1, 1, 1]).All().AreEqualTo(1);\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also use a ",(0,s.jsx)(n.a,{href:"/docs/expectations/object#custom-comparer",children:"custom comparer"})," or\nconfigure ",(0,s.jsx)(n.a,{href:"/docs/expectations/object#equivalence",children:"equivalence"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<MyClass> values = //...\nMyClass expected = //...\n\nawait Expect.That(values).All().AreEqualTo(expected).Equivalent();\nawait Expect.That(values).All().AreEqualTo(expected).Using(new MyClassComparer());\n"})}),"\n",(0,s.jsxs)(n.p,{children:["For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing\nwhite-space, or use a custom ",(0,s.jsx)(n.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:'await Expect.That(["foo", "FOO", "Foo"]).All().AreEqualTo("foo").IgnoringCase();\n'})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h2,{id:"all-be-unique",children:"All be unique"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that all items in a collection are unique."}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"await Expect.That([1, 2, 3]).AreAllUnique();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.p,{children:"For dictionaries, this expectation only verifies the values, as the keys are unique by design:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IDictionary<int, int> subject = new Dictionary<int, int>\n{\n  { 1, 1 },\n  { 2, 1 }\n};\n\n// This following expectation will fail, even though the keys are unique!\nawait Expect.That(subject).AreAllUnique();\n"})}),"\n",(0,s.jsx)(n.h2,{id:"elements",children:"Elements"}),"\n",(0,s.jsx)(n.p,{children:"You can add expectations that a certain number of elements must meet."}),"\n",(0,s.jsx)(n.h3,{id:"comply-with",children:"Comply with"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that items in a collection comply with an expectation on the individual elements:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"await Expect.That([1, 2, 3]).All().ComplyWith(item => item.IsLessThan(4));\nawait Expect.That([1, 2, 3]).AtLeast(2).ComplyWith(item => item.IsGreaterThanOrEqualTo(2));\nawait Expect.That([1, 2, 3]).AtMost(1).ComplyWith(item => item.IsNegative());\nawait Expect.That([1, 2, 3]).Between(2).And(3).ComplyWith(item => item.IsPositive());\nawait Expect.That([1, 2, 3]).Exactly(1).ComplyWith(item => item.IsEqualTo(2));\nawait Expect.That([1, 2, 3]).None().ComplyWith(item => item.IsNegative());\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"satisfy",children:"Satisfy"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that items in a collection satisfy a condition:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"await Expect.That([1, 2, 3]).All().Satisfy(item => item < 4);\nawait Expect.That([1, 2, 3]).AtLeast(2).Satisfy(item => item >= 2);\nawait Expect.That([1, 2, 3]).AtMost(1).Satisfy(item => item < 0);\nawait Expect.That([1, 2, 3]).Between(2).And(3).Satisfy(item => item > 0);\nawait Expect.That([1, 2, 3]).Exactly(1).Satisfy(item => item == 2);\nawait Expect.That([1, 2, 3]).None().Satisfy(item => item < 0);\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h2,{id:"sort-order",children:"Sort order"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that the collection contains is sorted in ascending or descending order:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:'await Expect.That([1, 2, 3]).IsInAscendingOrder();\nawait Expect.That(["c", "b", "a"]).IsInDescendingOrder();\n'})}),"\n",(0,s.jsx)(n.p,{children:"You can also specify a custom comparer:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:'await Expect.That(["a", "B", "c"]).IsInAscendingOrder().Using(StringComparer.OrdinalIgnoreCase);\n'})}),"\n",(0,s.jsx)(n.p,{children:"For objects, you can also verify the sort order on a member:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"MyClass[] values = //...\n\nawait Expect.That(values).IsInAscendingOrder(x => x.Value);\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h2,{id:"contain",children:"Contain"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that the collection contains a specific item or not:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Contains(13);\nawait Expect.That(values).DoesNotContain(42);\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also set occurrence constraints on ",(0,s.jsx)(n.code,{children:"Contain"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [1, 1, 1, 2];\n\nawait Expect.That(values).Contains(1).AtLeast(2.Times());\nawait Expect.That(values).Contains(1).Exactly(3.Times());\nawait Expect.That(values).Contains(1).AtMost(4.Times());\nawait Expect.That(values).Contains(1).Between(1).And(5.Times());\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also use a ",(0,s.jsx)(n.a,{href:"/docs/expectations/object#custom-comparer",children:"custom comparer"})," or\nconfigure ",(0,s.jsx)(n.a,{href:"/docs/expectations/object#equivalence",children:"equivalence"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<MyClass> values = //...\nMyClass expected = //...\n\nawait Expect.That(values).Contains(expected).Equivalent();\nawait Expect.That(values).Contains(expected).Using(new MyClassComparer());\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"predicate",children:"Predicate"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that the collection contains an item that satisfies a condition:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Contains(x => x > 12 && x < 14);\nawait Expect.That(values).DoesNotContain(x => x >= 42);\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also set occurrence constraints on ",(0,s.jsx)(n.code,{children:"Contain"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [1, 1, 1, 2];\n\nawait Expect.That(values).Contains(x => x == 1).AtLeast(2.Times());\nawait Expect.That(values).Contains(x => x == 1).Exactly(3.Times());\nawait Expect.That(values).Contains(x => x == 1).AtMost(4.Times());\nawait Expect.That(values).Contains(x => x == 1).Between(1).And(5.Times());\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"contain-subset",children:"Contain subset"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that a collection contains another collection as a subset:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).Contains([1, 2]);\nawait Expect.That(values).Contains([3, 2]).InAnyOrder();\nawait Expect.That(values).Contains([1, 1, 2, 2]).IgnoringDuplicates();\nawait Expect.That(values).Contains([3, 3, 1, 1]).InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsxs)(n.p,{children:["To check for a proper subset, append ",(0,s.jsx)(n.code,{children:".Properly()"})," (which would fail for equal collections)."]}),"\n",(0,s.jsx)(n.h3,{id:"be-contained-in",children:"Be contained in"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that a collection is contained in another collection (it is a superset):"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).IsContainedIn([1, 2, 3, 4]);\nawait Expect.That(values).IsContainedIn([4, 3, 2, 1]).InAnyOrder();\nawait Expect.That(values).IsContainedIn([1, 1, 2, 2, 3, 3, 4, 4]).IgnoringDuplicates();\nawait Expect.That(values).IsContainedIn([4, 4, 3, 3, 2, 2, 1, 1]).InAnyOrder().IgnoringDuplicates();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsxs)(n.p,{children:["To check for a proper superset, append ",(0,s.jsx)(n.code,{children:".Properly()"})," (which would fail for equal collections)."]}),"\n",(0,s.jsx)(n.h2,{id:"start-with",children:"Start with"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, if a collection starts with another collection or not:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 3);\n\nawait Expect.That(values).StartsWith(1, 2);\nawait Expect.That(values).DoesNotStartWith(2, 3);\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also use a ",(0,s.jsx)(n.a,{href:"/docs/expectations/object#custom-comparer",children:"custom comparer"})," or\nconfigure ",(0,s.jsx)(n.a,{href:"/docs/expectations/object#equivalence",children:"equivalence"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<MyClass> values = //...\nMyClass expected = //...\n\nawait Expect.That(values).StartsWith(expected).Equivalent();\nawait Expect.That(values).StartsWith(expected).Using(new MyClassComparer());\n"})}),"\n",(0,s.jsxs)(n.p,{children:["For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing\nwhite-space, or use a custom ",(0,s.jsx)(n.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:'await Expect.That(["FOO", "BAR"]).StartsWith(["foo"]).IgnoringCase();\n'})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h2,{id:"end-with",children:"End with"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, if a collection ends with another collection or not:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 5);\n\nawait Expect.That(values).EndsWith(4, 5);\nawait Expect.That(values).DoesNotEndWith(3, 5);\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also use a ",(0,s.jsx)(n.a,{href:"/docs/expectations/object#custom-comparer",children:"custom comparer"})," or\nconfigure ",(0,s.jsx)(n.a,{href:"/docs/expectations/object#equivalence",children:"equivalence"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<MyClass> values = //...\nMyClass expected = //...\n\nawait Expect.That(values).EndsWith(expected).Equivalent();\nawait Expect.That(values).EndsWith(expected).Using(new MyClassComparer());\n"})}),"\n",(0,s.jsxs)(n.p,{children:["For strings, you can configure this expectation to ignore case, ignore newline style, ignoring leading or trailing\nwhite-space, or use a custom ",(0,s.jsx)(n.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:'await Expect.That(["FOO", "BAR"]).EndsWith(["bar"]).IgnoringCase();\n'})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsx)(n.em,{children:"Caution: this method will always have to completely materialize the enumerable!"})}),"\n",(0,s.jsx)(n.h2,{id:"have",children:"Have"}),"\n",(0,s.jsx)(n.p,{children:"Specifications that count the elements in a collection."}),"\n",(0,s.jsx)(n.h3,{id:"all",children:"All"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that all items in the collection, satisfy an expectation:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).All().Satisfy(i => i <= 20);\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"at-least",children:"At least"}),"\n",(0,s.jsxs)(n.p,{children:["You can verify, that at least ",(0,s.jsx)(n.code,{children:"minimum"})," items in the collection, satisfy an expectation:"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).AtLeast(9).Satisfy(i => i < 10);\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also verify, that the collection has at least ",(0,s.jsx)(n.code,{children:"minimum"})," items:"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Has().AtLeast(9).Items();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"at-most",children:"At most"}),"\n",(0,s.jsxs)(n.p,{children:["You can verify, that at most ",(0,s.jsx)(n.code,{children:"maximum"})," items in the collection, satisfy an expectation:"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).AtMost(1).Satisfy(i => i < 2);\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also verify, that the collection has at most ",(0,s.jsx)(n.code,{children:"maximum"})," items:"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Has().AtMost(11).Items();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"between",children:"Between"}),"\n",(0,s.jsxs)(n.p,{children:["You can verify, that between ",(0,s.jsx)(n.code,{children:"minimum"})," and ",(0,s.jsx)(n.code,{children:"maximum"})," items in the collection, satisfy an expectation:"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Between(1).And(2).Satisfy(i => i < 2);\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also verify, that the collection has between ",(0,s.jsx)(n.code,{children:"minimum"})," and ",(0,s.jsx)(n.code,{children:"maximum"})," items:"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Has().Between(9).And(11).Items();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"exactly",children:"Exactly"}),"\n",(0,s.jsxs)(n.p,{children:["You can verify, that exactly ",(0,s.jsx)(n.code,{children:"expected"})," items in the collection, satisfy an expectation:"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).Exactly(9).Satisfy(i => i < 10);\n"})}),"\n",(0,s.jsxs)(n.p,{children:["You can also verify, that the collection has exactly ",(0,s.jsx)(n.code,{children:"expected"})," items:"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 10);\n\nawait Expect.That(values).Has().Exactly(10).Items();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h3,{id:"none",children:"None"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that not item in the collection, satisfies an expectation:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Enumerable.Range(1, 20);\n\nawait Expect.That(values).None().Satisfy(i => i > 20);\n"})}),"\n",(0,s.jsx)(n.p,{children:"You can also verify, that the collection is empty."}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = Array.Empty<int>();\n\nawait Expect.That(values).IsEmpty();\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectations works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h2,{id:"have-single",children:"Have single"}),"\n",(0,s.jsx)(n.p,{children:"You can verify, that the collection contains a single element that satisfies an expectation."}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [42];\n\nawait Expect.That(values).HasSingle();\nawait Expect.That(values).HasSingle().Which.IsGreaterThan(41);\n"})}),"\n",(0,s.jsx)(n.p,{children:"The awaited result is the single element:"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:"IEnumerable<int> values = [42];\n\nint result = await Expect.That(values).HasSingle();\nawait Expect.That(result).IsGreaterThan(41);\n"})}),"\n",(0,s.jsx)(n.p,{children:(0,s.jsxs)(n.em,{children:["Note: The same expectation works also for ",(0,s.jsx)(n.code,{children:"IAsyncEnumerable<T>"}),"."]})}),"\n",(0,s.jsx)(n.h2,{id:"dictionaries",children:"Dictionaries"}),"\n",(0,s.jsx)(n.h3,{id:"contain-keys",children:"Contain key(s)"}),"\n",(0,s.jsxs)(n.p,{children:["You can verify, that a dictionary contains the ",(0,s.jsx)(n.code,{children:"expected"})," key(s):"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:'Dictionary<int, string> values = new() { { 42, "foo" }, { 43, "bar" } };\n\nawait Expect.That(values).ContainsKey(42);\nawait Expect.That(values).ContainsKeys(42, 43);\nawait Expect.That(values).DoesNotContainKey(44);\nawait Expect.That(values).DoesNotContainKeys(44, 45, 46);\n'})}),"\n",(0,s.jsx)(n.p,{children:"You can add additional expectations on the corresponding value(s):"}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:'Dictionary<int, string> values = new() { { 42, "foo" }, { 43, "bar" }, { 44, "baz" } };\n\nawait Expect.That(values).ContainsKey(42).WhoseValue.IsEqualTo("foo");\nawait Expect.That(values).ContainsKeys(43, 44).WhoseValues.ComplyWith(v => v.StartsWith("ba"));\n'})}),"\n",(0,s.jsx)(n.h3,{id:"contain-values",children:"Contain value(s)"}),"\n",(0,s.jsxs)(n.p,{children:["You can verify, that a dictionary contains the ",(0,s.jsx)(n.code,{children:"expected"})," value(s):"]}),"\n",(0,s.jsx)(n.pre,{children:(0,s.jsx)(n.code,{className:"language-csharp",children:'Dictionary<int, string> values = new() { { 42, "foo" }, { 43, "bar" } };\n\nawait Expect.That(values).ContainsValue("foo");\nawait Expect.That(values).ContainsValues("foo", "bar");\nawait Expect.That(values).DoesNotContainValue("something");\nawait Expect.That(values).DoesNotContainValues("something", "else");\n'})})]})}function d(e={}){const{wrapper:n}={...(0,i.R)(),...e.components};return n?(0,s.jsx)(n,{...e,children:(0,s.jsx)(h,{...e})}):h(e)}},8453:(e,n,a)=>{a.d(n,{R:()=>c,x:()=>l});var t=a(6540);const s={},i=t.createContext(s);function c(e){const n=t.useContext(i);return t.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function l(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(s):e.components||s:c(e.components),t.createElement(i.Provider,{value:n},e.children)}}}]);