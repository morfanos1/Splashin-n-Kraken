using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour {
	// Inner radius, rocks do not appear inside this circle.
	public float InnerRadius;
	// Outer radius, rocks appear inside this circle.
	public float OuterRadius;
	// Distance between rocks.
	public float RockSpacing;
	// Object to spawn as rocks.
	public GameObject Rock;

	void Start() {
		this.Rock.SetActive(false);
		this.Generate();
	}

	void Generate() {
		foreach (Transform child in this.transform) {
			Object.Destroy(child.gameObject);
		}
		List<Vector2> rocks = RockSet.Generate(this.InnerRadius, this.OuterRadius, this.RockSpacing);
		foreach (Vector2 pos in rocks) {
			GameObject rock = Object.Instantiate(this.Rock, new Vector3(pos.x, pos.y, 0.0f), Quaternion.identity, this.transform);
			rock.SetActive(true);
		}
	}

	void AddRock(float x, float y) {
		GameObject rock = Object.Instantiate(this.Rock, new Vector3(x, y), Quaternion.identity, this.transform);
		rock.SetActive(true);
	}

	private class RockSet {
		float innerRadius;
		float outerRadius;
		float spacing;
		float spacing2;
		float worldToGrid;
		int gridCount;
		int[,] grid;
		List<Vector2> items;
		Queue<Vector2> queue;
		const int maxSamples = 15;

		private RockSet() {
		}

		public static List<Vector2> Generate(float innerRadius, float outerRadius, float spacing) {
			RockSet s = new RockSet();
			s.innerRadius = innerRadius;
			s.outerRadius = outerRadius;
			s.spacing = spacing;
			s.worldToGrid = Mathf.Sqrt(2) / spacing;
			s.gridCount = Mathf.CeilToInt(2.0f * outerRadius * s.worldToGrid);
			s.grid = new int[s.gridCount, s.gridCount];
			s.items = new List<Vector2>();
			s.queue = new Queue<Vector2>();

			float angle = Random.Range(-Mathf.PI, Mathf.PI);
			s.TryAdd(new Vector2(innerRadius * Mathf.Cos(angle), innerRadius * Mathf.Sin(angle)));
			for (int i = 0; i < 10000 && s.queue.Count > 0; i++) {
				s.TryAdd(s.queue.Dequeue());
			}

			return s.items;
		}

		private void TryAdd(Vector2 pos) {
			int xi = Mathf.FloorToInt((pos.x + this.outerRadius) * this.worldToGrid);
			int yi = Mathf.FloorToInt((pos.y + this.outerRadius) * this.worldToGrid);
			if (xi < 0 || xi >= this.gridCount || yi < 0 || yi >= this.gridCount) {
				return;
			}
			if (this.grid[xi, yi] > 0) {
				return;
			}
			int x0 = Mathf.FloorToInt((pos.x + this.outerRadius - this.spacing) * this.worldToGrid);
			int y0 = Mathf.FloorToInt((pos.y + this.outerRadius - this.spacing) * this.worldToGrid);
			int x1 = Mathf.CeilToInt((pos.x + this.outerRadius + this.spacing) * this.worldToGrid) + 1;
			int y1 = Mathf.CeilToInt((pos.y + this.outerRadius + this.spacing) * this.worldToGrid) + 1;
			x0 = Mathf.Max(0, x0);
			y0 = Mathf.Max(0, y0);
			x1 = Mathf.Min(this.gridCount, x1);
			y1 = Mathf.Min(this.gridCount, y1);
			float spacing2 = this.spacing * this.spacing;
			for (int xt = x0; xt < x1; xt++) {
				for (int yt = y0; yt < y1; yt++) {
					int idx = this.grid[xt, yt];
					if (idx == 0) {
						continue;
					}
					Vector2 v = this.items[idx - 1];
					float dx = pos.x - v.x, dy = pos.y - v.y;
					float r2 = dx * dx + dy * dy;
					if (r2 < spacing2) {
						return;
					}
				}
			}
			this.items.Add(pos);
			this.grid[xi, yi] = this.items.Count;
			for (int i = 0; i < maxSamples; i++) {
				float angle = Random.Range(-Mathf.PI, Mathf.PI);
				float d = this.spacing * 1.01f;
				Vector2 npos = new Vector2(pos.x + Mathf.Cos(angle) * d, pos.y + Mathf.Sin(angle) * d);
				float r2 = npos.x * npos.x + npos.y * npos.y;
				if (r2 >= this.innerRadius * this.innerRadius && r2 <= this.outerRadius * this.outerRadius) {
					this.queue.Enqueue(npos);
				}
			}
		}
	}
}
