import http from "k6/http";
import { sleep } from "k6";

export const options = {
  vus: 20, // 20 virtual users
  duration: "15s", // run for 15 seconds
};

export default function () {
  const url =
    "https://bp-calculator-prashidi-dggsdtf3fhd6b2hd.northeurope-01.azurewebsites.net";

  http.get(url);

  sleep(1);
}
