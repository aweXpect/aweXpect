window.BENCHMARK_DATA = {
  "lastUpdate": 1731871717457,
  "repoUrl": "https://github.com/Testably/Testably.Expectations",
  "entries": {
    "Benchmark.Net Benchmark": [
      {
        "commit": {
          "author": {
            "name": "Valentin Breuß",
            "username": "vbreuss",
            "email": "vbreuss@gmail.com"
          },
          "committer": {
            "name": "GitHub",
            "username": "web-flow",
            "email": "noreply@github.com"
          },
          "id": "f161f267486dbd4e645c4fda8c137686b9113772",
          "message": "Update build.yml",
          "timestamp": "2024-11-10T10:10:22Z",
          "url": "https://github.com/Testably/Testably.Expectations/commit/f161f267486dbd4e645c4fda8c137686b9113772"
        },
        "date": 1731233719633,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 223.71290619032723,
            "unit": "ns",
            "range": "± 0.9824994791885284"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 293.10680683453876,
            "unit": "ns",
            "range": "± 1.0800133745774851"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 646.4990128835042,
            "unit": "ns",
            "range": "± 2.459467255687573"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 404.90180892944335,
            "unit": "ns",
            "range": "± 2.780572939295524"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 446.45077065059115,
            "unit": "ns",
            "range": "± 1.4857544364505064"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1355.23776108878,
            "unit": "ns",
            "range": "± 10.779287028811426"
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
          "id": "372d9673affeb0e12faca6e1c4b2ebd2a8c2446d",
          "message": "feat: add initial benchmark project (#149)\n\nFrom #147:\nAdd initial benchmarks for comparison between `TUnit.Assertions`,\n`FluentAssertions` and this project:\n- Happy Case for string should be equal\n- Happy Case for bool should be true",
          "timestamp": "2024-11-10T11:23:45+01:00",
          "tree_id": "4bcdabd6b8a04ba093b75a735bbeaa5028e8e741",
          "url": "https://github.com/Testably/Testably.Expectations/commit/372d9673affeb0e12faca6e1c4b2ebd2a8c2446d"
        },
        "date": 1731234426740,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 220.91200603757585,
            "unit": "ns",
            "range": "± 1.6673221741150637"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 287.3064190864563,
            "unit": "ns",
            "range": "± 1.8987624217673886"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 657.3793855813833,
            "unit": "ns",
            "range": "± 2.017588380190715"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 391.971413675944,
            "unit": "ns",
            "range": "± 2.2096639792726607"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 438.5044598238809,
            "unit": "ns",
            "range": "± 1.5135455385680785"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1396.38973903656,
            "unit": "ns",
            "range": "± 6.76543995059669"
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
          "id": "fcea0df51d34eaf0896efaf1b77746d3a6f9f64d",
          "message": "feat: add `TimeOnly` expectation (#150)\n\nFrom #127:\r\nAdd `TimeOnly` expectations:\r\n- `BeAfter` / `NotBeAfter`\r\n- `BeOnOrAfter` / `NotBeOnOrAfter`\r\n- `BeBefore` / `NotBeBefore`\r\n- `BeOnOrBefore` / `NotBeOnOrBefore`",
          "timestamp": "2024-11-10T11:00:24Z",
          "tree_id": "56512c8efb26c65a3705dd8962ba2ae60489583e",
          "url": "https://github.com/Testably/Testably.Expectations/commit/fcea0df51d34eaf0896efaf1b77746d3a6f9f64d"
        },
        "date": 1731236636838,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 221.24531308809915,
            "unit": "ns",
            "range": "± 2.7567662285223835"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 290.2114628278292,
            "unit": "ns",
            "range": "± 1.38390463018621"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 656.8612679072788,
            "unit": "ns",
            "range": "± 5.822501986901396"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 411.2567177500044,
            "unit": "ns",
            "range": "± 3.2486096660092723"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 447.9265311559041,
            "unit": "ns",
            "range": "± 5.013208854186453"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1355.0822670800346,
            "unit": "ns",
            "range": "± 9.529984021371916"
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
          "id": "bb7c1007414be398350ea5ac3a1d0a1a5f0c0370",
          "message": "refactor: consolidate `#if` statements (#151)\n\nConsolidate the `#if` statements to only use positive statements.\nAdd test for `Expect.Should()` with expectationbuilder callback",
          "timestamp": "2024-11-10T13:25:06Z",
          "tree_id": "7a3e71b93711f6d9c5d862a50b6cee7db698ea3e",
          "url": "https://github.com/Testably/Testably.Expectations/commit/bb7c1007414be398350ea5ac3a1d0a1a5f0c0370"
        },
        "date": 1731245317670,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 231.95924600533075,
            "unit": "ns",
            "range": "± 2.009931400826811"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 309.1715818132673,
            "unit": "ns",
            "range": "± 3.4751504840649656"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 670.4935470727773,
            "unit": "ns",
            "range": "± 3.399065626447531"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 409.7361055374146,
            "unit": "ns",
            "range": "± 6.891673280914807"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 449.8361392974854,
            "unit": "ns",
            "range": "± 3.3563971734779092"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1396.362138748169,
            "unit": "ns",
            "range": "± 7.339711269690868"
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
          "id": "73a0a6b2e695a040116fc421a3cde618908bdcf1",
          "message": "refactor: make chronology tests more resilient (#152)\n\nMake Chronology-Tests more resilient by adjusting min-max values of\r\nrandom current time to allow +/- 100 seconds (days) to be a valid value",
          "timestamp": "2024-11-10T14:03:53Z",
          "tree_id": "96287d5de77a996ce1cdf61a928d39d4648ece00",
          "url": "https://github.com/Testably/Testably.Expectations/commit/73a0a6b2e695a040116fc421a3cde618908bdcf1"
        },
        "date": 1731247633378,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 224.43843685663663,
            "unit": "ns",
            "range": "± 0.9345790027157462"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 292.308470694224,
            "unit": "ns",
            "range": "± 2.4638372765151053"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 650.4342516581218,
            "unit": "ns",
            "range": "± 6.050968907664431"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 403.79427777017867,
            "unit": "ns",
            "range": "± 1.1080549765914078"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 447.2884476001446,
            "unit": "ns",
            "range": "± 2.7409025463825003"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1434.351681436811,
            "unit": "ns",
            "range": "± 8.93975378025529"
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
          "id": "04a9795ad43498a480332a1409e6d697a250691f",
          "message": "feat: Add workflow for github pages (#153)",
          "timestamp": "2024-11-10T14:16:51Z",
          "tree_id": "1ab3a4cdb4e24d76cae6e3e60c62d1ce7f8d91d8",
          "url": "https://github.com/Testably/Testably.Expectations/commit/04a9795ad43498a480332a1409e6d697a250691f"
        },
        "date": 1731248403363,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 216.46007132530212,
            "unit": "ns",
            "range": "± 1.3224365529877649"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 284.57064822514855,
            "unit": "ns",
            "range": "± 2.5388069569838216"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 631.8529075895037,
            "unit": "ns",
            "range": "± 3.630213696092942"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 386.3392347971598,
            "unit": "ns",
            "range": "± 1.7363159762152305"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 440.9900715192159,
            "unit": "ns",
            "range": "± 2.9611957094779924"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1386.7418332417806,
            "unit": "ns",
            "range": "± 10.945158104530364"
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
          "id": "1aaca7885876b26244830eab4dbc177c57b309b5",
          "message": "refactor: separate expectations and tests in Chronology to their respective types (#154)\n\nMove the expectations and tests under `Chronology` to their respective\r\ntypes. This affects:\r\n- DateTimes\r\n- DateTimeOffsets\r\n- DateOnlys\r\n- TimeOnlys\r\n- TimeSpans",
          "timestamp": "2024-11-10T16:04:32Z",
          "tree_id": "b317949549092bfd580f051e1871e367ae880ff2",
          "url": "https://github.com/Testably/Testably.Expectations/commit/1aaca7885876b26244830eab4dbc177c57b309b5"
        },
        "date": 1731254871015,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 216.72207025119238,
            "unit": "ns",
            "range": "± 1.1580089025899505"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 288.9929835796356,
            "unit": "ns",
            "range": "± 1.0541912406379452"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 644.4516975696271,
            "unit": "ns",
            "range": "± 2.703445356688473"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 389.57822058995566,
            "unit": "ns",
            "range": "± 1.9400942176669025"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 435.29317934172497,
            "unit": "ns",
            "range": "± 0.764834763495559"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1418.9397478103638,
            "unit": "ns",
            "range": "± 7.880048282000287"
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
          "id": "0f42c1497bf4539d14f27d11c122be39098dfcb1",
          "message": "feat: add `EditorBrowsable` attribute (#155)\n\nExclude `Equals`, `GetHashCode`, `ToString()`, ... from IntelliSense via\r\nthe\r\n[`EditorBrowsable`](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.editorbrowsableattribute?view=net-8.0)\r\nattribute.",
          "timestamp": "2024-11-10T17:56:34Z",
          "tree_id": "8f409ee6717041cf4b1ea81d05dd83abcea80667",
          "url": "https://github.com/Testably/Testably.Expectations/commit/0f42c1497bf4539d14f27d11c122be39098dfcb1"
        },
        "date": 1731261593609,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 220.7546913964408,
            "unit": "ns",
            "range": "± 1.5986074895835443"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 283.0423843065898,
            "unit": "ns",
            "range": "± 0.8866866603560025"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 635.3028348286947,
            "unit": "ns",
            "range": "± 1.5456776976335178"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 394.8485062462943,
            "unit": "ns",
            "range": "± 1.5713895946307697"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 438.8522559483846,
            "unit": "ns",
            "range": "± 3.3113918977091616"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1401.9338188171387,
            "unit": "ns",
            "range": "± 8.363523236401408"
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
          "id": "0f42c1497bf4539d14f27d11c122be39098dfcb1",
          "message": "feat: add `EditorBrowsable` attribute (#155)\n\nExclude `Equals`, `GetHashCode`, `ToString()`, ... from IntelliSense via\r\nthe\r\n[`EditorBrowsable`](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.editorbrowsableattribute?view=net-8.0)\r\nattribute.",
          "timestamp": "2024-11-10T17:56:34Z",
          "tree_id": "8f409ee6717041cf4b1ea81d05dd83abcea80667",
          "url": "https://github.com/Testably/Testably.Expectations/commit/0f42c1497bf4539d14f27d11c122be39098dfcb1"
        },
        "date": 1731264248904,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 233.62158036231995,
            "unit": "ns",
            "range": "± 1.05900778647767"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 298.9362648010254,
            "unit": "ns",
            "range": "± 2.1146670716291016"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 656.9032852808634,
            "unit": "ns",
            "range": "± 5.569730610098333"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 419.9620007106236,
            "unit": "ns",
            "range": "± 3.029939867418661"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 454.4908361117045,
            "unit": "ns",
            "range": "± 2.590958323166978"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1386.6543276860164,
            "unit": "ns",
            "range": "± 14.150717333218816"
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
          "id": "b2092af98504ef23635a8dce8b5424925d7c5053",
          "message": "feat: add support for nullable `DateTimeOffset` (#157)\n\nSupport nullable `DateTimeOffset` with the `Be` method",
          "timestamp": "2024-11-12T14:48:56Z",
          "tree_id": "ccb2017e31325f0bd9be761c4e8ab404f588bbfd",
          "url": "https://github.com/Testably/Testably.Expectations/commit/b2092af98504ef23635a8dce8b5424925d7c5053"
        },
        "date": 1731423133826,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 226.72857233456202,
            "unit": "ns",
            "range": "± 1.8332837540218576"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 293.8473629951477,
            "unit": "ns",
            "range": "± 2.5392937672265554"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 663.0240126291911,
            "unit": "ns",
            "range": "± 5.848658211248516"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 404.99270076018115,
            "unit": "ns",
            "range": "± 2.694549927127409"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 481.8928180422102,
            "unit": "ns",
            "range": "± 3.679040222960862"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1389.2814937319074,
            "unit": "ns",
            "range": "± 10.298525390392117"
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
          "id": "917e22276ef7a6edd15ac0a3b65ddb97ef8f054d",
          "message": "docs: add link to benchmarks in readme (#158)\n\nAdd a link to the benchmark page in the README.md.",
          "timestamp": "2024-11-12T16:09:11+01:00",
          "tree_id": "781f0967e7e6ec9427b7975445bde0144020af81",
          "url": "https://github.com/Testably/Testably.Expectations/commit/917e22276ef7a6edd15ac0a3b65ddb97ef8f054d"
        },
        "date": 1731424373335,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 227.77836092880793,
            "unit": "ns",
            "range": "± 2.1592757750130773"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 315.20230801900226,
            "unit": "ns",
            "range": "± 1.3527706019479002"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 645.7986221313477,
            "unit": "ns",
            "range": "± 3.774623254360758"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 416.2192128499349,
            "unit": "ns",
            "range": "± 2.8192670558064177"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 447.3453282356262,
            "unit": "ns",
            "range": "± 3.211755590066838"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1353.339626566569,
            "unit": "ns",
            "range": "± 9.252369462216903"
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
          "id": "ca1a84365295428b139b60c2a77a2d8cc567dee8",
          "message": "feat: add `BeAfter`/`BeBefore` for `DateTimeOffset` (#159)\n\nfrom #129 \r\nAdd the following expectations for `DateTimeOffset`:\r\n- `BeAfter` / `NotBeAfter`\r\n- `BeBefore` / `NotBeBefore`\r\n- `BeOnOrAfter` / `NotBeOnOrAfter`\r\n- `BeOnOrBefore` / `NotBeOnOrBefore`",
          "timestamp": "2024-11-12T21:50:40+01:00",
          "tree_id": "93945dfa7ace7b1d75ca91a40c6ae188e78378d4",
          "url": "https://github.com/Testably/Testably.Expectations/commit/ca1a84365295428b139b60c2a77a2d8cc567dee8"
        },
        "date": 1731444855379,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 221.4791898557118,
            "unit": "ns",
            "range": "± 1.4948364989355416"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 294.70770638783773,
            "unit": "ns",
            "range": "± 0.697049865646878"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 659.6057526906332,
            "unit": "ns",
            "range": "± 3.158920204838963"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 408.0745369706835,
            "unit": "ns",
            "range": "± 3.0217100818199536"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 464.6016073593727,
            "unit": "ns",
            "range": "± 2.6284276165390876"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1385.073644365583,
            "unit": "ns",
            "range": "± 6.600621095593919"
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
          "id": "5610a0464f73e320374c006574eb59bde93e0843",
          "message": "feat: add `HaveHour`, `HaveMinute`, `HaveSecond` and `HaveMillisecond` for `TimeOnly` (#160)\n\nFrom #127\r\nAdd the following expectations for `TimeOnly`:\r\n- `HaveHour` / `NotHaveHour`\r\n- `HaveMinute` / `NotHaveMinute`\r\n- `HaveSecond` / `NotHaveSecond`\r\n- `HaveMilliseconds` / `NotHaveMilliseconds`",
          "timestamp": "2024-11-12T21:17:45Z",
          "tree_id": "091e11b3cd1ba771596fe70621fc19dfbb33f6fe",
          "url": "https://github.com/Testably/Testably.Expectations/commit/5610a0464f73e320374c006574eb59bde93e0843"
        },
        "date": 1731446478645,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 214.94696274825506,
            "unit": "ns",
            "range": "± 2.020114838166824"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 283.4445134162903,
            "unit": "ns",
            "range": "± 2.260906417998972"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 645.6803504943848,
            "unit": "ns",
            "range": "± 3.9873336401091084"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 395.1602522986276,
            "unit": "ns",
            "range": "± 3.009594144936853"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 444.192113049825,
            "unit": "ns",
            "range": "± 1.3048804047477256"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1472.8937675476075,
            "unit": "ns",
            "range": "± 10.219067944030963"
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
          "id": "3bf7cd352e31230bdd42dd389953f4c1e87d40c1",
          "message": "fix: tolerance for `NotBeAfter.Within`/`NotBeBefore.Within` (#161)\n\nThe tolerance for\r\n`NotBeAfter`/`NotBeBefore`/`NotBeOnOrAfter`/`NotBeOnOrBefore` is not\r\napplied correctly for `DateOnly` and `TimeOnly`.",
          "timestamp": "2024-11-13T08:48:30Z",
          "tree_id": "f09eaf58d0d84996fbfef528ef7a83be2d63ad0d",
          "url": "https://github.com/Testably/Testably.Expectations/commit/3bf7cd352e31230bdd42dd389953f4c1e87d40c1"
        },
        "date": 1731487918094,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 212.83286545826837,
            "unit": "ns",
            "range": "± 1.987063116119944"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 286.1541848182678,
            "unit": "ns",
            "range": "± 0.6103587652700636"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 624.3998407217173,
            "unit": "ns",
            "range": "± 2.2554885581593886"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 395.4768909386226,
            "unit": "ns",
            "range": "± 3.71923301138482"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 433.5152122633798,
            "unit": "ns",
            "range": "± 2.0986990654166453"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1443.6281449635824,
            "unit": "ns",
            "range": "± 7.26617561797841"
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
          "id": "111637b4c65a733cf3fbfec63ce0107c81f6cd2b",
          "message": "feat: add property expectations for DateOnly (#162)\n\nAdd property expectations for `DateOnly`:\n- `HaveYear` / `NotHaveYear`\n- `HaveMonth` / `NotHaveMonth`\n- `HaveDay` / `NotHaveDay`",
          "timestamp": "2024-11-13T11:45:33+01:00",
          "tree_id": "1d3a2ce68204d5b842a5719628ad5fe9de47c917",
          "url": "https://github.com/Testably/Testably.Expectations/commit/111637b4c65a733cf3fbfec63ce0107c81f6cd2b"
        },
        "date": 1731494939911,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 212.1721953323909,
            "unit": "ns",
            "range": "± 2.105996343461402"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 281.2347474416097,
            "unit": "ns",
            "range": "± 1.8250576792886504"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 622.898862361908,
            "unit": "ns",
            "range": "± 5.350347696632538"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 388.22877007264356,
            "unit": "ns",
            "range": "± 1.3219861048639947"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 437.63180742263796,
            "unit": "ns",
            "range": "± 1.8585543531814483"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1422.3460948944091,
            "unit": "ns",
            "range": "± 8.100348022980032"
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
          "id": "d558faa0a6228d8d85078f3cf373389a7bddec1c",
          "message": "feat: add property expectations for `DateTime` (#163)\n\nFrom #129 \r\nAdd property expectations for `DateTime`:\r\n- `HaveYear` / `NotHaveYear`\r\n- `HaveMonth` / `NotHaveMonth`\r\n- `HaveDay` / `NotHaveDay`\r\n- `HaveHour` / `NotHaveHour`\r\n- `HaveMinute` / `NotHaveMinute`\r\n- `HaveSecond` / `NotHaveSecond`\r\n- `HaveMillisecond` / `NotHaveMillisecond`\r\n- `HaveKind` / `NotHaveKind`",
          "timestamp": "2024-11-13T16:53:54Z",
          "tree_id": "ad5d62c9c35f79c697cca0c4f66c5365a2e0cc55",
          "url": "https://github.com/Testably/Testably.Expectations/commit/d558faa0a6228d8d85078f3cf373389a7bddec1c"
        },
        "date": 1731517046974,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 218.46944171648758,
            "unit": "ns",
            "range": "± 0.9212096230855528"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 287.0030254999796,
            "unit": "ns",
            "range": "± 2.2694541331430864"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 648.1974428176879,
            "unit": "ns",
            "range": "± 4.038969515570516"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 405.1878536860148,
            "unit": "ns",
            "range": "± 1.6887134391708276"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 445.07006107966106,
            "unit": "ns",
            "range": "± 2.4537874700148774"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1411.7127881368,
            "unit": "ns",
            "range": "± 8.889775299787747"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "valentin.breuss@baur.eu",
            "name": "Valentin",
            "username": "vbreuss"
          },
          "committer": {
            "email": "valentin.breuss@baur.eu",
            "name": "Valentin",
            "username": "vbreuss"
          },
          "distinct": true,
          "id": "9e02c6d0787d84d33a3d4816c2377e14b2fcbb05",
          "message": "Accept API changes",
          "timestamp": "2024-11-13T18:14:32+01:00",
          "tree_id": "802e6d4c92f3695ac3a29322aa58eb03c8489166",
          "url": "https://github.com/Testably/Testably.Expectations/commit/9e02c6d0787d84d33a3d4816c2377e14b2fcbb05"
        },
        "date": 1731518300828,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 220.46039595603943,
            "unit": "ns",
            "range": "± 2.023001843162665"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 287.9714822428567,
            "unit": "ns",
            "range": "± 2.1428997489521593"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 644.030905859811,
            "unit": "ns",
            "range": "± 3.379216987988673"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 404.94679979177624,
            "unit": "ns",
            "range": "± 3.9843889672190635"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 441.65681438446046,
            "unit": "ns",
            "range": "± 3.2533183404410835"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1473.0798961639405,
            "unit": "ns",
            "range": "± 15.450736132033347"
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
          "id": "d558faa0a6228d8d85078f3cf373389a7bddec1c",
          "message": "feat: add property expectations for `DateTime` (#163)\n\nFrom #129 \r\nAdd property expectations for `DateTime`:\r\n- `HaveYear` / `NotHaveYear`\r\n- `HaveMonth` / `NotHaveMonth`\r\n- `HaveDay` / `NotHaveDay`\r\n- `HaveHour` / `NotHaveHour`\r\n- `HaveMinute` / `NotHaveMinute`\r\n- `HaveSecond` / `NotHaveSecond`\r\n- `HaveMillisecond` / `NotHaveMillisecond`\r\n- `HaveKind` / `NotHaveKind`",
          "timestamp": "2024-11-13T16:53:54Z",
          "tree_id": "ad5d62c9c35f79c697cca0c4f66c5365a2e0cc55",
          "url": "https://github.com/Testably/Testably.Expectations/commit/d558faa0a6228d8d85078f3cf373389a7bddec1c"
        },
        "date": 1731518600599,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 221.41491038458688,
            "unit": "ns",
            "range": "± 1.5486652024676313"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 299.83895448275973,
            "unit": "ns",
            "range": "± 1.5598612464245447"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 632.2568460305532,
            "unit": "ns",
            "range": "± 2.9425459675772787"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 406.358073870341,
            "unit": "ns",
            "range": "± 2.752151913557011"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 443.15258383750916,
            "unit": "ns",
            "range": "± 2.1013058983508563"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1456.1561321258546,
            "unit": "ns",
            "range": "± 12.61342434970214"
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
          "id": "65c4bdcdfb56414e9ef8974ccd48b590bd92cebb",
          "message": "feat: add property expectations for `DateTimeOffset` (#164)\n\nFrom #129 \r\nAdd property expectations for `DateTimeOffset`:\r\n- `HaveYear` / `NotHaveYear`\r\n- `HaveMonth` / `NotHaveMonth`\r\n- `HaveDay` / `NotHaveDay`\r\n- `HaveHour` / `NotHaveHour`\r\n- `HaveMinute` / `NotHaveMinute`\r\n- `HaveSecond` / `NotHaveSecond`\r\n- `HaveMillisecond` / `NotHaveMillisecond`\r\n- `HaveOffset` / `NotHaveOffset`",
          "timestamp": "2024-11-13T17:28:46Z",
          "tree_id": "4e5a125c3e80f4296ea23d84b87721ac9f7b1b66",
          "url": "https://github.com/Testably/Testably.Expectations/commit/65c4bdcdfb56414e9ef8974ccd48b590bd92cebb"
        },
        "date": 1731519135349,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 225.3312591654914,
            "unit": "ns",
            "range": "± 1.4556992135942795"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 302.34123277664185,
            "unit": "ns",
            "range": "± 2.431968406727903"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 648.3094309488932,
            "unit": "ns",
            "range": "± 6.194114855247609"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 414.2573178927104,
            "unit": "ns",
            "range": "± 4.290156000637056"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 448.17269931520735,
            "unit": "ns",
            "range": "± 3.1161662913750274"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1398.634435926165,
            "unit": "ns",
            "range": "± 11.61620473614626"
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
          "id": "2e01cc7ddb12c241354656ae56e24cb07ac2dc9f",
          "message": "feat: add `BeGreaterThan` / `BeLessThan` for `TimeSpan` (#165)\n\nFrom #128:\r\nAdd the following expectations for `TimeSpan`:\r\n- `BeGreaterThan` / `NotBeGreaterThan`\r\n- `BeGreaterThanOrEqualTo` / `NotBeGreaterThanOrEqualTo`\r\n- `BeLessThan` / `NotBeLessThan`\r\n- `BeLessThanOrEqualTo` / `NotBeLessThanOrEqualTo`\r\nand add `Within` extension for `.Be`\r\n\r\nAlso implement the methods for nullable `TimeSpan`",
          "timestamp": "2024-11-13T21:04:39Z",
          "tree_id": "c2ddf49e115048b5127e325c1f2e1c7c35bd5ac6",
          "url": "https://github.com/Testably/Testably.Expectations/commit/2e01cc7ddb12c241354656ae56e24cb07ac2dc9f"
        },
        "date": 1731532092139,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 211.97621334516086,
            "unit": "ns",
            "range": "± 0.8429299564988316"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 328.85947792870655,
            "unit": "ns",
            "range": "± 0.7103551950578009"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 628.4535245895386,
            "unit": "ns",
            "range": "± 1.4055703612745338"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 391.13799883524575,
            "unit": "ns",
            "range": "± 4.587484160524376"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 448.2648733139038,
            "unit": "ns",
            "range": "± 2.3397301058108453"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1347.9602160135905,
            "unit": "ns",
            "range": "± 7.208101378171359"
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
          "id": "924a0afeb1133d6dae5bf5787f7ed327db2d1523",
          "message": "feat: add `BePositive`/`BeNegative` for `TimeSpan` (#166)\n\nFixes #128 \r\nAdd the following expectations for `TimeSpan`:\r\n- `BePositive` / `NotBePositive`\r\n- `BeNegative` / `NotBeNegative`",
          "timestamp": "2024-11-13T21:52:47Z",
          "tree_id": "f8e50ad348605e58f45629cf818f6aa11d33c981",
          "url": "https://github.com/Testably/Testably.Expectations/commit/924a0afeb1133d6dae5bf5787f7ed327db2d1523"
        },
        "date": 1731534981109,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 216.2116312469755,
            "unit": "ns",
            "range": "± 1.202956064615306"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 317.9863357861837,
            "unit": "ns",
            "range": "± 1.594073344681572"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 630.2645305906024,
            "unit": "ns",
            "range": "± 1.6264945991797388"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 392.36237713268827,
            "unit": "ns",
            "range": "± 2.476951175994892"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 437.10160195032756,
            "unit": "ns",
            "range": "± 2.6676824314512144"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1356.7474951426188,
            "unit": "ns",
            "range": "± 11.261237902041866"
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
          "id": "fad8683aa15fb525c79548d65c36c3828dbeb77e",
          "message": "feat: change the `.For` syntax (#167)\n\nChange the `.For` syntax to work on `IExpectSubject<T>` instead of\r\n`IThat<T>`\r\nThis allows using inheritance of `IThat<T>` to reduce duplications.\r\nThe syntax changes from\r\n```csharp\r\nawait That(sut).Should().For<MyClass, int>(x => x.Value, v => v.Be(4))`.And.For<MyClass, int>(x => x.Value2, v => v.Be(6));\r\n```\r\nto\r\n```csharp\r\nawait That(sut).For(x => x.Value, v => v.Should().Be(4)).And.For(x => x.Value2, v => v.Should().Be(6));\r\n```",
          "timestamp": "2024-11-15T17:15:52Z",
          "tree_id": "68ee15bfb0734eb185619601432356a3edf143ae",
          "url": "https://github.com/Testably/Testably.Expectations/commit/fad8683aa15fb525c79548d65c36c3828dbeb77e"
        },
        "date": 1731691153255,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 217.02692226001196,
            "unit": "ns",
            "range": "± 1.1902252122837893"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 260.37234385808307,
            "unit": "ns",
            "range": "± 2.7518069176254794"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 628.5911921819051,
            "unit": "ns",
            "range": "± 3.8754191911761477"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 392.5958313624064,
            "unit": "ns",
            "range": "± 2.8527776501441973"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 408.55137392452787,
            "unit": "ns",
            "range": "± 2.1653411930096413"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1397.0270998636881,
            "unit": "ns",
            "range": "± 7.509081577875937"
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
          "id": "bf78a7f968d525e896bc96839e0210ae6ba938bd",
          "message": "feat: enable duplicate `Which` expectations (#168)\n\nSupport for multiple `Which` statements that are combined via AND.",
          "timestamp": "2024-11-15T19:10:35+01:00",
          "tree_id": "661d008e3f3e30177f38f51386e486847d7c9565",
          "url": "https://github.com/Testably/Testably.Expectations/commit/bf78a7f968d525e896bc96839e0210ae6ba938bd"
        },
        "date": 1731694430232,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 216.1184001139232,
            "unit": "ns",
            "range": "± 1.738875595604143"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 250.2024953548725,
            "unit": "ns",
            "range": "± 0.5335104645967235"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 658.4788731166294,
            "unit": "ns",
            "range": "± 2.52920052215773"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 392.6614631017049,
            "unit": "ns",
            "range": "± 2.1863735203123102"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 412.0789326349894,
            "unit": "ns",
            "range": "± 2.4007084017881635"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1384.2194468634468,
            "unit": "ns",
            "range": "± 9.545150290563988"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "name": "Valentin Breuß",
            "username": "vbreuss",
            "email": "vbreuss@gmail.com"
          },
          "committer": {
            "name": "GitHub",
            "username": "web-flow",
            "email": "noreply@github.com"
          },
          "id": "e9220a6687023806fe2d1b47a5b8636f9b576bc6",
          "message": "Update build.yml",
          "timestamp": "2024-11-15T19:14:53Z",
          "url": "https://github.com/Testably/Testably.Expectations/commit/e9220a6687023806fe2d1b47a5b8636f9b576bc6"
        },
        "date": 1731698336889,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 222.46153000684885,
            "unit": "ns",
            "range": "± 1.0834850881235625"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 268.9526172320048,
            "unit": "ns",
            "range": "± 2.5376889708327237"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 641.5090964635214,
            "unit": "ns",
            "range": "± 3.810084234117204"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 406.8690660158793,
            "unit": "ns",
            "range": "± 2.910119577275534"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 415.4075540224711,
            "unit": "ns",
            "range": "± 3.069679079165074"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1426.2461572374616,
            "unit": "ns",
            "range": "± 8.278917831093647"
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
          "id": "0a5f3c884883d6f54f1593839c8c057d390790fb",
          "message": "fix: pipeline permissions (#171)",
          "timestamp": "2024-11-15T20:22:14+01:00",
          "tree_id": "5bc63e667e39b6e97bb570a37d44ed3780512d47",
          "url": "https://github.com/Testably/Testably.Expectations/commit/0a5f3c884883d6f54f1593839c8c057d390790fb"
        },
        "date": 1731698750262,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 221.1428900082906,
            "unit": "ns",
            "range": "± 2.948699103455059"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 263.79806769688923,
            "unit": "ns",
            "range": "± 2.291320168459724"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 659.553221975054,
            "unit": "ns",
            "range": "± 3.6157875095612697"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 408.70627828439075,
            "unit": "ns",
            "range": "± 3.5452634080315613"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 421.3244773546855,
            "unit": "ns",
            "range": "± 2.2883467406081794"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1408.4683787027996,
            "unit": "ns",
            "range": "± 3.1478133835538062"
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
          "id": "30b9dcbcc98ecefd4bc71dfa822a40360877931f",
          "message": "chore(deps): update TUnit to 0.2.212 (#174)\n\nUpdate `TUnit` to\r\n[v0.2.212](https://www.nuget.org/packages/TUnit/0.2.212) for benchmarks",
          "timestamp": "2024-11-16T07:09:09Z",
          "tree_id": "a1b1345d437a333a360e47f85aa220392eb55900",
          "url": "https://github.com/Testably/Testably.Expectations/commit/30b9dcbcc98ecefd4bc71dfa822a40360877931f"
        },
        "date": 1731741155583,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 215.01213223593575,
            "unit": "ns",
            "range": "± 1.250381226540841"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 252.36867221196493,
            "unit": "ns",
            "range": "± 1.9043782378539118"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 650.4219693456378,
            "unit": "ns",
            "range": "± 6.5837774573699"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 388.10698921339855,
            "unit": "ns",
            "range": "± 2.30799181562082"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 407.81992365519204,
            "unit": "ns",
            "range": "± 2.526176910542036"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1344.627413204738,
            "unit": "ns",
            "range": "± 9.401642266771349"
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
          "id": "fc21505b4b8eac5b73e1378219648a9a1cc45f7d",
          "message": "refactor: remove superfluous result texts in `NotThrow` (#175)\n\nFixes #173",
          "timestamp": "2024-11-16T14:48:21+01:00",
          "tree_id": "0d8d1a47c9d534ded3c3ea869e2d3d1966c2938e",
          "url": "https://github.com/Testably/Testably.Expectations/commit/fc21505b4b8eac5b73e1378219648a9a1cc45f7d"
        },
        "date": 1731765111208,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 219.55042430332728,
            "unit": "ns",
            "range": "± 1.2833513917020125"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 257.3822285945599,
            "unit": "ns",
            "range": "± 0.49555173290207444"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 646.0605608133169,
            "unit": "ns",
            "range": "± 1.583992664471235"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 398.47285439417914,
            "unit": "ns",
            "range": "± 2.187707911335047"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 403.61925758634294,
            "unit": "ns",
            "range": "± 3.877565797576163"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1386.0766773223877,
            "unit": "ns",
            "range": "± 2.6624076456453887"
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
          "id": "8beb42f4b0e6260113ca891e38447bc3e573796d",
          "message": "refactor: remove unused `Invert` method (#176)\n\nThe `Invert` method on `ConstraintResult` is not used anywhere in code",
          "timestamp": "2024-11-16T13:54:34Z",
          "tree_id": "a757474756c999bb1a8b4025b449cba944da2007",
          "url": "https://github.com/Testably/Testably.Expectations/commit/8beb42f4b0e6260113ca891e38447bc3e573796d"
        },
        "date": 1731765483504,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 211.62036009935233,
            "unit": "ns",
            "range": "± 1.77423650583222"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 257.4515151977539,
            "unit": "ns",
            "range": "± 3.2645310382733834"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 633.3295285361154,
            "unit": "ns",
            "range": "± 5.874662859746229"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 394.9920405069987,
            "unit": "ns",
            "range": "± 4.457586395030007"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 400.2075576464335,
            "unit": "ns",
            "range": "± 3.174228283573669"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 1405.1853749411446,
            "unit": "ns",
            "range": "± 7.382877604250353"
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
          "id": "e185f476d056aa782931ea0900c1aa557efdef5d",
          "message": "chore(deps): update TUnit to 0.3.3 (#177)",
          "timestamp": "2024-11-16T20:30:53Z",
          "tree_id": "29c7bcce4e8357c6a84d219b6fd8b8f398007fe6",
          "url": "https://github.com/Testably/Testably.Expectations/commit/e185f476d056aa782931ea0900c1aa557efdef5d"
        },
        "date": 1731789258903,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 216.33164218493872,
            "unit": "ns",
            "range": "± 1.5859347449960324"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 262.0833044052124,
            "unit": "ns",
            "range": "± 1.0299069256362403"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 595.3232270081838,
            "unit": "ns",
            "range": "± 1.6411602097891167"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 393.95939789499556,
            "unit": "ns",
            "range": "± 1.4322864952058616"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 406.61336354414624,
            "unit": "ns",
            "range": "± 0.6449988009894985"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 794.5678090367999,
            "unit": "ns",
            "range": "± 3.2759009464021784"
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
          "id": "e150650b9fe0eeecbfcbc850097c2b515bccafd8",
          "message": "refactor: improve `Formatter` performance (#178)\n\nReplace static `Formatter.Format` method with instance method and add\r\noverloads for common types to speedup formatting",
          "timestamp": "2024-11-16T22:05:51+01:00",
          "tree_id": "40e7a2ae0be4936760ca4b7e04450cfea5b89ee9",
          "url": "https://github.com/Testably/Testably.Expectations/commit/e150650b9fe0eeecbfcbc850097c2b515bccafd8"
        },
        "date": 1731791344706,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 214.96680693967002,
            "unit": "ns",
            "range": "± 1.2937204378837455"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 160.33751722744532,
            "unit": "ns",
            "range": "± 1.307942499776674"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 595.7693546840122,
            "unit": "ns",
            "range": "± 3.8406527846304876"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 390.27749789555867,
            "unit": "ns",
            "range": "± 2.5073824882874365"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 279.3786124865214,
            "unit": "ns",
            "range": "± 2.2438731822096174"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 795.8874958583286,
            "unit": "ns",
            "range": "± 5.470391889304692"
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
          "id": "d98e758041ad2af919ecb720544fbfa232c4460f",
          "message": "feat: improve unclear error messages when accessing properties (#180)\n\nFixes #172 by using the subject name (default \"it\") in the error\r\nmessage.",
          "timestamp": "2024-11-17T09:45:35Z",
          "tree_id": "09e02f5b82e34675397437455e2a468fa535280d",
          "url": "https://github.com/Testably/Testably.Expectations/commit/d98e758041ad2af919ecb720544fbfa232c4460f"
        },
        "date": 1731836945110,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 217.64562688668568,
            "unit": "ns",
            "range": "± 1.995035274341335"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 166.19945885453905,
            "unit": "ns",
            "range": "± 0.9235219202998374"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 629.0879451887948,
            "unit": "ns",
            "range": "± 2.0206415073753576"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 399.73716831207275,
            "unit": "ns",
            "range": "± 2.255723335171622"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 332.300386873881,
            "unit": "ns",
            "range": "± 2.43931110370214"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 808.0915812810262,
            "unit": "ns",
            "range": "± 3.3732781730901014"
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
          "id": "29720dd622fc0f7e3fb5c4e71deeb243fefa128d",
          "message": "refactor: add XML comments for hidden methods (#181)\n\nAdd XML-Doc comments for methods hidden with the\n`EditorBrowsableAttribute` to clarify, why this should not be visible in\ncode suggestions.",
          "timestamp": "2024-11-17T11:26:43Z",
          "tree_id": "e2a717c228f5b22b08f13e0f90e62f45611e0c3a",
          "url": "https://github.com/Testably/Testably.Expectations/commit/29720dd622fc0f7e3fb5c4e71deeb243fefa128d"
        },
        "date": 1731843017380,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 221.70008981227875,
            "unit": "ns",
            "range": "± 2.0800750678201325"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 169.06887694767542,
            "unit": "ns",
            "range": "± 1.7926497901847653"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 603.2111625671387,
            "unit": "ns",
            "range": "± 3.7606178228637197"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 410.6040994008382,
            "unit": "ns",
            "range": "± 3.604313807870807"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 332.18479473250255,
            "unit": "ns",
            "range": "± 2.2478781331853623"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 805.1359320368085,
            "unit": "ns",
            "range": "± 6.981612291095164"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "committer": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "id": "c6b4d955e84e2f11c0f2313a5cbc58be888f9775",
          "message": "Add prepare release step",
          "timestamp": "2024-11-17T13:23:33Z",
          "url": "https://github.com/Testably/Testably.Expectations/commit/c6b4d955e84e2f11c0f2313a5cbc58be888f9775"
        },
        "date": 1731852625885,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 217.81503044641934,
            "unit": "ns",
            "range": "± 1.0312733575155357"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 174.52498316764832,
            "unit": "ns",
            "range": "± 0.6583946896310079"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 650.480433066686,
            "unit": "ns",
            "range": "± 1.2304935783475461"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 396.63674589565824,
            "unit": "ns",
            "range": "± 1.527203318026829"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 330.30152349472047,
            "unit": "ns",
            "range": "± 2.899898278006628"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 782.5740074157715,
            "unit": "ns",
            "range": "± 5.603351895752466"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "committer": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "id": "929b50268b42552c136f940785c11568c6ea8846",
          "message": "Add test possibility",
          "timestamp": "2024-11-17T14:06:22Z",
          "url": "https://github.com/Testably/Testably.Expectations/commit/929b50268b42552c136f940785c11568c6ea8846"
        },
        "date": 1731852944410,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 216.9914049735436,
            "unit": "ns",
            "range": "± 1.5942835705763048"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 167.08693205515544,
            "unit": "ns",
            "range": "± 1.020267381718128"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 585.4639973640442,
            "unit": "ns",
            "range": "± 4.802013846741965"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 401.9297550519307,
            "unit": "ns",
            "range": "± 3.1270887821023625"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 326.5420263835362,
            "unit": "ns",
            "range": "± 1.8127359911399583"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 810.3256729443868,
            "unit": "ns",
            "range": "± 2.0389356060784727"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "name": "Valentin Breuß",
            "username": "vbreuss",
            "email": "vbreuss@gmail.com"
          },
          "committer": {
            "name": "GitHub",
            "username": "web-flow",
            "email": "noreply@github.com"
          },
          "id": "1c878edaa0217b669a5826cce0b7bf83e6243095",
          "message": "Update build.yml",
          "timestamp": "2024-11-17T14:21:23Z",
          "url": "https://github.com/Testably/Testably.Expectations/commit/1c878edaa0217b669a5826cce0b7bf83e6243095"
        },
        "date": 1731853505966,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 208.13954281806946,
            "unit": "ns",
            "range": "± 3.054716861422659"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 162.04476041793822,
            "unit": "ns",
            "range": "± 1.6349269894340894"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 564.5351823398045,
            "unit": "ns",
            "range": "± 7.9667090473896645"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 380.9216799736023,
            "unit": "ns",
            "range": "± 3.1466042665169804"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 320.3938416481018,
            "unit": "ns",
            "range": "± 3.384333941466389"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 758.5696219035557,
            "unit": "ns",
            "range": "± 6.381882732543741"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "committer": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "id": "1d30043dc3bba454d48275488427d692605164fa",
          "message": "Fix build error",
          "timestamp": "2024-11-17T15:38:42Z",
          "url": "https://github.com/Testably/Testably.Expectations/commit/1d30043dc3bba454d48275488427d692605164fa"
        },
        "date": 1731858300092,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 217.56924399307795,
            "unit": "ns",
            "range": "± 1.102127765365684"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 171.14934005737305,
            "unit": "ns",
            "range": "± 1.1924971646627867"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 582.43523645401,
            "unit": "ns",
            "range": "± 3.1509591280907085"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 393.4142058690389,
            "unit": "ns",
            "range": "± 2.6101371099481008"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 334.07972586949666,
            "unit": "ns",
            "range": "± 2.130747361035853"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 834.0054317201887,
            "unit": "ns",
            "range": "± 4.1276945578919655"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "committer": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "id": "5885b730ef1c8a2b5a3223f2e88d2209b4351fd5",
          "message": "Use gh",
          "timestamp": "2024-11-17T15:50:16Z",
          "url": "https://github.com/Testably/Testably.Expectations/commit/5885b730ef1c8a2b5a3223f2e88d2209b4351fd5"
        },
        "date": 1731858848666,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 222.2992928504944,
            "unit": "ns",
            "range": "± 3.0710284748479144"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 169.13469398938693,
            "unit": "ns",
            "range": "± 0.569830623827247"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 655.5969656626384,
            "unit": "ns",
            "range": "± 4.014102257000108"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 408.044668896993,
            "unit": "ns",
            "range": "± 2.5775668217481176"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 336.73320606776645,
            "unit": "ns",
            "range": "± 2.2047980416435715"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 805.9541246550424,
            "unit": "ns",
            "range": "± 9.209128593423278"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "committer": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "id": "9ad34bb184f577450618b254c50d3d12e1134826",
          "message": "Run gh pr create -B main -H integrate/merge-benchmarks-to-main --title 'docs: update benchmarks while releasing ${version}'\nmust provide `--title` and `--body` (or `--fill` or `fill-first` or `--fillverbose`) when not running interactively",
          "timestamp": "2024-11-17T15:56:33Z",
          "url": "https://github.com/Testably/Testably.Expectations/commit/9ad34bb184f577450618b254c50d3d12e1134826"
        },
        "date": 1731859234012,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 222.4651799996694,
            "unit": "ns",
            "range": "± 2.3233201887460315"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 169.67758461634318,
            "unit": "ns",
            "range": "± 0.800379740291146"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 576.2298515637716,
            "unit": "ns",
            "range": "± 0.9853321343905764"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 406.0632034937541,
            "unit": "ns",
            "range": "± 3.199514752591007"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 330.8764753341675,
            "unit": "ns",
            "range": "± 1.8843026532522409"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 825.0888361930847,
            "unit": "ns",
            "range": "± 3.2029172304444202"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "committer": {
            "name": "Valentin",
            "username": "vbreuss",
            "email": "valentin.breuss@baur.eu"
          },
          "id": "21c217bcdbcab1aff8bc81a0b3bb403ac26c0ffc",
          "message": "Store benchmark only on main branch",
          "timestamp": "2024-11-17T16:08:51Z",
          "url": "https://github.com/Testably/Testably.Expectations/commit/21c217bcdbcab1aff8bc81a0b3bb403ac26c0ffc"
        },
        "date": 1731859862809,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 219.65330357551574,
            "unit": "ns",
            "range": "± 2.3711766126064133"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 167.5044539996556,
            "unit": "ns",
            "range": "± 1.1327203437232738"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 586.9698716572353,
            "unit": "ns",
            "range": "± 7.888708521872029"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 410.2769337654114,
            "unit": "ns",
            "range": "± 2.654251981934292"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 327.0249419552939,
            "unit": "ns",
            "range": "± 2.1510868616508816"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 863.6240283966065,
            "unit": "ns",
            "range": "± 4.4423998353434655"
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
          "id": "55475c4ec69189249736902de49ab2725a17f3ce",
          "message": "feat: add prepare release step to build pipeline (#183)",
          "timestamp": "2024-11-17T17:28:04+01:00",
          "tree_id": "774ca879bc0338bfbfd19348ac367b76892b79d8",
          "url": "https://github.com/Testably/Testably.Expectations/commit/55475c4ec69189249736902de49ab2725a17f3ce"
        },
        "date": 1731861090849,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 219.55243284361703,
            "unit": "ns",
            "range": "± 1.8681059807093012"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 169.27850273450215,
            "unit": "ns",
            "range": "± 1.249913386432451"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 584.9937897409711,
            "unit": "ns",
            "range": "± 2.469376309657369"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 400.8521567491385,
            "unit": "ns",
            "range": "± 2.4417536001745046"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 323.15083690790027,
            "unit": "ns",
            "range": "± 0.7713848544248805"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 822.1176720399123,
            "unit": "ns",
            "range": "± 2.444117865958926"
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
          "id": "7087313d77aba084d530734fd5cfe103bfccea70",
          "message": "fix: set git user (#185)",
          "timestamp": "2024-11-17T17:53:17Z",
          "tree_id": "c269e55f332ed916764998b71ea74c21b75bd839",
          "url": "https://github.com/Testably/Testably.Expectations/commit/7087313d77aba084d530734fd5cfe103bfccea70"
        },
        "date": 1731866200720,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 215.07127609619727,
            "unit": "ns",
            "range": "± 1.1218016551096175"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 164.38755606015522,
            "unit": "ns",
            "range": "± 1.1811065872353075"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 600.2323092051914,
            "unit": "ns",
            "range": "± 3.82254679334926"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 389.0008321205775,
            "unit": "ns",
            "range": "± 1.0624059618181243"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 327.50405575434365,
            "unit": "ns",
            "range": "± 2.7179260311569613"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 770.3729868616376,
            "unit": "ns",
            "range": "± 3.3562891115176083"
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
          "id": "5c88080ca5b6564141b787af614da733caf5f0f8",
          "message": "fix: force push after rebase (#186)",
          "timestamp": "2024-11-17T20:25:02+01:00",
          "tree_id": "b13e7ec22972b5366eeb7a481941873fb758f31f",
          "url": "https://github.com/Testably/Testably.Expectations/commit/5c88080ca5b6564141b787af614da733caf5f0f8"
        },
        "date": 1731871715867,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_FluentAssertions",
            "value": 215.97997997488295,
            "unit": "ns",
            "range": "± 1.6049674377347174"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TestablyExpectations",
            "value": 163.8051788330078,
            "unit": "ns",
            "range": "± 1.5642542826225387"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.Bool_TUnit",
            "value": 594.8003665288289,
            "unit": "ns",
            "range": "± 5.979679205657919"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_FluentAssertions",
            "value": 390.9245591163635,
            "unit": "ns",
            "range": "± 1.4591402825518052"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TestablyExpectations",
            "value": 319.4690228144328,
            "unit": "ns",
            "range": "± 2.7385031991925284"
          },
          {
            "name": "Testably.Expectations.Benchmarks.HappyCaseBenchmarks.String_TUnit",
            "value": 793.6264349619547,
            "unit": "ns",
            "range": "± 5.410210146424123"
          }
        ]
      }
    ]
  }
}