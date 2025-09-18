import hre from "hardhat";
import fs from "fs";
import path from "path";
import {fileURLToPath} from "url";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

async function main() {
	const Store = await hre.ethers.getContractFactory("Store");
	const store = await Store.deploy();
	await store.waitForDeployment();

	const address = await store.getAddress();
	console.log("Store deployed to:", address);

	const artifact = await hre.artifacts.readArtifact("Store");
	const config = {address, abi: artifact.abi};

	const outPath = path.resolve(__dirname, "..", "..", "web", "wwwroot", "contractConfig.json");
	fs.mkdirSync(path.dirname(outPath), {recursive: true});
	fs.writeFileSync(outPath, JSON.stringify(config, null, 2));
	console.log("Wrote config to:", outPath);
}

main().catch((e) => {
	console.error(e);
	process.exit(1);
});
