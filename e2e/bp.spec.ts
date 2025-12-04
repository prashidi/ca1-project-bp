import { test, expect } from "@playwright/test";

test.describe("Blood Pressure Calculator - E2E Tests", () => {
  // LOW BLOOD PRESSURE TEST
  test("should return Low Blood Pressure for 85/55", async ({ page }) => {
    await page.goto("/");
    await page.getByLabel("Systolic").fill("85");
    await page.getByLabel("Diastolic").fill("55");
    await page.getByRole("button", { name: "Submit" }).click();

    await expect(page.getByText("Low Blood Pressure")).toBeVisible();
  });

  // IDEAL BLOOD PRESSURE TEST
  test("should return Ideal Blood Pressure for 110/70", async ({ page }) => {
    await page.goto("/");
    await page.getByLabel("Systolic").fill("110");
    await page.getByLabel("Diastolic").fill("70");
    await page.getByRole("button", { name: "Submit" }).click();

    await expect(page.getByText("Ideal Blood Pressure")).toBeVisible();
  });

  // PRE-HIGH BLOOD PRESSURE TEST
  test("should return Pre-High Blood Pressure for 130/85", async ({ page }) => {
    await page.goto("/");
    await page.getByLabel("Systolic").fill("130");
    await page.getByLabel("Diastolic").fill("85");
    await page.getByRole("button", { name: "Submit" }).click();

    await expect(page.getByText("Pre-High Blood Pressure")).toBeVisible();
  });

  // HIGH BLOOD PRESSURE TEST
  test("should return High Blood Pressure for 150/95", async ({ page }) => {
    await page.goto("/");
    await page.getByLabel("Systolic").fill("150");
    await page.getByLabel("Diastolic").fill("95");
    await page.getByRole("button", { name: "Submit" }).click();

    await expect(page.getByText("High Blood Pressure")).toBeVisible();
  });
});
