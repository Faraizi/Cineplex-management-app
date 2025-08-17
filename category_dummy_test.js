// Minimal simulation of the Angular component logic for dummy testing

function getUserId(userValue) {
	if (Array.isArray(userValue)) {
		return userValue?.[0]?.id ?? null;
	}
	return userValue?.id ?? null;
}

function validateForm(form) {
	if (!form) return { valid: false, reason: 'no form' };
	const { code, description, total } = form;
	if (!code || typeof code !== 'string' || code.trim().length === 0 || code.length > 50) {
		return { valid: false, reason: 'invalid code' };
	}
	if (!description || typeof description !== 'string' || description.trim().length === 0 || description.length > 200) {
		return { valid: false, reason: 'invalid description' };
	}
	if (typeof total !== 'number' || !Number.isFinite(total) || total < 1) {
		return { valid: false, reason: 'invalid total' };
	}
	return { valid: true };
}

function buildPayload(stateCategory, formValue, userValue) {
	const validation = validateForm(formValue);
	if (!validation.valid) {
		throw new Error(`Form invalid: ${validation.reason}`);
	}

	const isEditing = !!(stateCategory && stateCategory.id);
	const userId = getUserId(userValue);
	return {
		id: isEditing ? stateCategory.id : 0,
		code: formValue.code,
		description: formValue.description,
		total: formValue.total,
		entryUserID: userId,
		isNew: !isEditing,
	};
}

// Simulated CategoryService.createCategory using a Promise
function createCategory(payload, shouldFail = false) {
	return new Promise((resolve, reject) => {
		setTimeout(() => {
			if (shouldFail) {
				reject(new Error('Network/API error'));
			} else {
				resolve({ ok: true, payload });
			}
		}, 50);
	});
}

async function run() {
	const cases = [];

	// Case 1: Create new (no state), object-shaped userValue
	cases.push({
		name: 'Create new (no state), userValue as object',
		stateCategory: null,
		form: { code: 'CAT001', description: 'Snacks', total: 5 },
		userValue: { id: 123 },
		shouldFail: false,
	});

	// Case 2: Edit (state has id)
	cases.push({
		name: 'Edit existing (state has id)',
		stateCategory: { id: 42, code: 'OLD', description: 'Old', total: 1 },
		form: { code: 'CAT002', description: 'Beverages', total: 10 },
		userValue: { id: 321 },
		shouldFail: false,
	});

	// Case 3: Create new, array-shaped userValue
	cases.push({
		name: 'Create new, userValue as array',
		stateCategory: null,
		form: { code: 'CAT003', description: 'Combos', total: 3 },
		userValue: [{ id: 456 }],
		shouldFail: false,
	});

	// Case 4: Error path (simulate API failure)
	cases.push({
		name: 'API failure on create',
		stateCategory: null,
		form: { code: 'CAT004', description: 'Merch', total: 2 },
		userValue: { id: 999 },
		shouldFail: true,
	});

	for (const c of cases) {
		try {
			const payload = buildPayload(c.stateCategory, c.form, c.userValue);
			const result = await createCategory(payload, c.shouldFail);
			const successMsg = payload.isNew ? 'Category created successfully' : 'Category updated successfully';
			console.log(`[PASS] ${c.name}`);
			console.log(`\tPayload: ${JSON.stringify(payload)}`);
			console.log(`\tMessage: ${successMsg}`);
		} catch (err) {
			const payload = (() => {
				try { return buildPayload(c.stateCategory, c.form, c.userValue); } catch { return null; }
			})();
			const action = payload && payload.isNew ? 'create' : 'update';
			const errorMsg = `Failed to ${action} category. Please try again.`;
			console.log(`[FAIL/EXPECTED] ${c.name}`);
			console.log(`\tError: ${err.message}`);
			console.log(`\tMessage: ${errorMsg}`);
		}
	}
}

run().catch(e => {
	console.error('Unexpected test runner error:', e);
	process.exit(1);
});