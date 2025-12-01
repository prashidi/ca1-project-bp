import { check, sleep } from "k6";
import http from "k6/http";

// Export an options object to configure how k6 will behave during test execution.
//
export let options = {
  // Stages: ramp-up → steady load → ramp-down
  stages: [
    { duration: "1m", target: 20 },
    { duration: "1m", target: 20 },
    { duration: "1m", target: 0 },
  ],

  // Set a threshold for 95th percentile response time
  thresholds: {
    http_req_duration: ["p(95) < 300"],
  },

  // Save response bodies (only GET forces text anyway)
  discardResponseBodies: false,

  cloud: {
    name: "BP Load Test",
    distribution: {
      distributionLabel2: { loadZone: "amazon:ie:dublin", percent: 100 },
    },
  },
};

/**

- Generate a random integer between min and max.
*/
function getRandomInt(min, max) {
  return Math.floor(Math.random() * (max - min + 1) + min);
}

const BASE_URL =
  "https://bp-calculator-prashidi-staging-euaec9evcje4fhcd.northeurope-01.azurewebsites.net/";

// Default VU function — entry point
export default function () {
  // STEP 1 — GET main page (force text to allow token extraction)
  let res = http.get(`${BASE_URL}/`, { responseType: "text" });

  check(res, {
    "GET / returns 200": (r) => r.status === 200,
  });

  // STEP 2 — Submit BP calculation form via POST
  const systolic = getRandomInt(70, 190).toString();
  const diastolic = getRandomInt(40, 100).toString();

  res = res.submitForm({
    fields: {
      Systolic: systolic,
      Diastolic: diastolic,
    },
    submit: "Calculate",
  });

  check(res, {
    "POST form returns 200": (r) => r.status === 200,
  });

  // Think time
  sleep(3);
}
