window.BENCHMARK_DATA = {
  "lastUpdate": 1732460915194,
  "repoUrl": "https://github.com/aweXpect/aweXpect",
  "entries": {
    "Benchmark.Net Benchmark": [
      {
        "commit": {
          "author": {
            "name": "aweXpect",
            "username": "aweXpect"
          },
          "committer": {
            "name": "aweXpect",
            "username": "aweXpect"
          },
          "id": "087a9d7cc35ef61ee0bf86be3d008c5cd444f90a",
          "message": "fix: deployment of GitHub pages",
          "timestamp": "2024-11-24T07:07:58Z",
          "url": "https://github.com/aweXpect/aweXpect/pull/18/commits/087a9d7cc35ef61ee0bf86be3d008c5cd444f90a"
        },
        "date": 1732451046172,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 224.51341882546743,
            "unit": "ns",
            "range": "± 1.843727701198027"
          },
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.Bool_aweXpect",
            "value": 168.71270225842792,
            "unit": "ns",
            "range": "± 0.6531414309737593"
          },
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 631.3338410513742,
            "unit": "ns",
            "range": "± 2.1479594622149567"
          },
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 393.7382577896118,
            "unit": "ns",
            "range": "± 2.4878681085251855"
          },
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.String_aweXpect",
            "value": 315.77155006848847,
            "unit": "ns",
            "range": "± 1.7719340159400114"
          },
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 776.9469863451444,
            "unit": "ns",
            "range": "± 5.794657013041029"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "vbreuss@gmail.com",
            "name": "Valentin Breuß",
            "username": "vbreuss"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "85172d373f904e1954572da06033fc78492b1bae",
          "message": "fix: benchmark commit (#21)\n\n* Test benchmark commit on CI pipeline\r\n* Set the ` --no-edit` flag\r\n* Move test to build.yml",
          "timestamp": "2024-11-24T15:04:32Z",
          "tree_id": "b12234489160abf3ea68a5735a25a257d6de4bc7",
          "url": "https://github.com/aweXpect/aweXpect/commit/85172d373f904e1954572da06033fc78492b1bae"
        },
        "date": 1732460914842,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 217.71777299472265,
            "unit": "ns",
            "range": "± 2.2180505666919856"
          },
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.Bool_aweXpect",
            "value": 168.94174247521622,
            "unit": "ns",
            "range": "± 0.7587249733944732"
          },
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 602.2305580774943,
            "unit": "ns",
            "range": "± 3.674379925715558"
          },
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 393.52738196055094,
            "unit": "ns",
            "range": "± 2.7549736663396893"
          },
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.String_aweXpect",
            "value": 314.2004409517561,
            "unit": "ns",
            "range": "± 1.3214116012298747"
          },
          {
            "name": "aweXpect.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 836.1589536031087,
            "unit": "ns",
            "range": "± 2.790730139831681"
          }
        ]
      }
    ]
  }
}