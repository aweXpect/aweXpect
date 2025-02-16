(function () {
	function init() {

		const main = document.getElementById('benchmarks-container');
		for (const name in window.BENCHMARK_DATA) {
			renderChart(main, name, window.BENCHMARK_DATA[name]);
		}
	}

	function renderChart(main, name, data) {
		const div = document.createElement("div");
		const h = document.createElement("h3");
		const l = document.createElement("a");
		l.setAttribute("href", "https://github.com/aweXpect/aweXpect/blob/main/Benchmarks/aweXpect.Benchmarks/HappyCaseBenchmarks." + name + ".cs");
		l.setAttribute("target", "_blank");
		const t = document.createTextNode(name);
		l.appendChild(t);
		h.appendChild(l);
		div.appendChild(h);
		const canvas = document.createElement('canvas');
		canvas.className = 'benchmark-chart';
		div.appendChild(canvas);
		main.appendChild(div);

		const options = {
			responsive: true,
			interaction: {
				mode: 'index',
				intersect: false,
			},
			stacked: false,
			plugins: {
				tooltip: {
					callbacks: {
						footer: items => {
							const commitSha = items[0].label;
							const commit = data.commits.find(x => x.sha.startsWith(commitSha));
							return '\n' + commit.message + '\n' + commit.date + ' by @' + commit.author;
						},
						label: (item) =>
							`${item.formattedValue} ${item.dataset.unit} | ${item.dataset.label}`,
					},
				},
			},
			scales: {
				y: {
					type: 'linear',
					display: true,
					title: { display: true, text: 'time [ns]' },
					position: 'left',
					min: 0
				},
				y1: {
					type: 'linear',
					display: true,
					title: { display: true, text: 'memory [bytes]' },
					position: 'right',
					min: 0,
					grid: {
						drawOnChartArea: false,
					},
				},
			}
		};
		new Chart(canvas, {
			type: 'line',
			data: data,
			options: options
		});
	}

	function waitForScripts() {
		if (typeof Chart !== 'undefined' &&
			typeof window.BENCHMARK_DATA !== 'undefined' &&
			window.BENCHMARK_DATA != null) {
			init();
		} else {
			setTimeout(waitForScripts, 100);
		}
	}

	waitForScripts();
})();
