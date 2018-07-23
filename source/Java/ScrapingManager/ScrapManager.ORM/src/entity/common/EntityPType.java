package entity.common;

import common.Column;

public abstract class EntityPType extends Entity {

	/*
	 * DEFAULT_P_KEY�� nosql���� ������ �˻��� �����ϰ� ó���ϱ� ����
	 */
	public static int DEFAULT_P_KEY = 0;
	@Column(name = "pkey", partitionkey = true)
	private int pkey;

	protected EntityPType(String key) {
		super(key);
		pkey = DEFAULT_P_KEY;
	}

	protected EntityPType(int pkey, String key) {
		super(key);
	}

	public int getPKey() {
		return pkey;
	}
}