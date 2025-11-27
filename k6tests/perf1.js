import http from "k6/http";
import { sleep } from "k6";

export const options = {
  //   vus: 20, // 20 virtual users
  //   duration: "15s", // run for 15 seconds

  // This stages configuration will ramp to 20 Virtual Users over a minute,
  // maintain those 20 concurrent users for 1 minute
  // then ramp down to 0 over a minute i.e. ramp-up pattern of "load"
  stages: [
    { duration: "1m", target: 20 }, // 1 new vu every 3 seconds
    { duration: "1m", target: 20 },
    { duration: "1m", target: 0 }, // 1 less vu every 3 seconds
  ],

  // set a threshold at 100 ms request duration for 95th percentile
  // request duration = time spent sending request, waiting for response, and receiving response
  // aka "response time"
  // the test will be marked as failed by threshold if the value is exceeded
  // i.e. 95% of request durations should be < 300 ms

  thresholds: {
    http_req_duration: ["p(95) < 300"],
  },

  // Don't save the bodies of HTTP responses by default, for improved performance
  // Can be overwritten by setting the `responseType` option to `text` or `binary` for individual requests
  discardResponseBodies: true,

  cloud: {
    distribution: {
      distributionLabel1: { loadZone: "amazon:us:ashburn", percent: 50 },
      distributionLabel2: { loadZone: "amazon:ie:dublin", percent: 50 },
    },
  },
};

// Export a default function - this defines the entry point for your VUs,
// similar to the main() function in many other languages.
export default function () {
  const res =
    "https://bp-calculator-prashidi-dggsdtf3fhd6b2hd.northeurope-01.azurewebsites.net";

  check(res, {
    "is status 200": (r) => r.status === 200,
  });

  // "think" for 3 seconds
  sleep(3);
}
