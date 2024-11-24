import React, {useEffect} from 'react';
import Layout from '@theme/Layout';

export default function Benchmarks() {
	useEffect(() => {
		const chartScript = document.createElement('script');
		chartScript.setAttribute('src', 'js/Chart.min.js');
		document.getElementById("__docusaurus").appendChild(chartScript);
		const dataScript = document.createElement('script');
		dataScript.setAttribute('src', 'js/data.js');
		document.getElementById("__docusaurus").appendChild(dataScript);
		const benchmarkScript = document.createElement('script');
		benchmarkScript.setAttribute('src', 'js/benchmarks.js');
		document.getElementById("__docusaurus").appendChild(benchmarkScript);
	}, []);
	return (
		<Layout title="Benchmarks" description="Benchmarks Page">
			<div
				style={{
					display: 'flex',
					justifyContent: 'center',
					alignItems: 'center',
					height: '50vh',
					fontSize: '20px',
				}}>
				<main id="main" />
			</div>
		</Layout>
	);
}
